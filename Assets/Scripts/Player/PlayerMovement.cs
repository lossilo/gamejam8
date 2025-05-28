using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float coyoteTime;
    [Tooltip("Must be a value between 0 and 1")] [SerializeField] private float coyoteTimeBufferMultiplier;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Ground Pound")]
    [SerializeField] private float groundPoundStrength;
    [SerializeField] private float groundPoundCheckLength;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int damageToEnemies;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;

    private bool moveBlock;
    private float currentMovementSpeed;
    private float currentCoyoteTime;
    private bool hasJumped;
    private bool canReCheckJump;
    private bool groundPounding;
    private bool startedMoving;
    private bool isMoving;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SoundEffectManager soundEffectManager;

    public bool MoveBlock { get { return moveBlock; } set { moveBlock = value; } }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        soundEffectManager = FindFirstObjectByType<SoundEffectManager>();

        currentMovementSpeed = movementSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
        SetCoyoteTime();
        PerformGroundPound();
    }

    private void Movement()
    {
        if (moveBlock)
        {
            playerRigidbody.linearVelocity = new Vector2(0, playerRigidbody.linearVelocity.y);
            playerAnimator.SetBool("IsWalking", false);
            return;
        }

        float inputFloat;

        if (currentMovementSpeed == dashSpeed)
        {
            inputFloat = transform.localScale.x;
        }
        else
        {
            inputFloat = Input.GetAxisRaw("Horizontal");
        }

        if (Mathf.Abs(inputFloat) > Mathf.Epsilon)
        {
            playerRigidbody.linearVelocity = new Vector2(inputFloat * currentMovementSpeed, playerRigidbody.linearVelocity.y);
            startedMoving = true;
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerRigidbody.linearVelocity = new Vector2(0, playerRigidbody.linearVelocity.y);
            startedMoving = false;
            playerAnimator.SetBool("IsWalking", false);
        }

        if (Mathf.Abs(playerRigidbody.linearVelocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.linearVelocity.x), transform.localScale.y);
        }

        if (startedMoving != isMoving)
        {
            if (startedMoving)
            {
                soundEffectManager.PlaySound(moveSound, true);
            }
            else
            {
                soundEffectManager.StopSound(moveSound);
            }
        }

        isMoving = startedMoving;
    }

    public void Jump()
    {
        if (!moveBlock && (GroundCheck() || currentCoyoteTime > 0) && !hasJumped)
        {
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 1000));
            playerAnimator.SetBool("IsJumping", true);
            hasJumped = true;
            soundEffectManager.PlaySound(jumpSound);
            Invoke("EnableReCheck", 0.2f);
        }
    }

    private void EnableReCheck()
    {
        canReCheckJump = true;
    }

    private void SetCoyoteTime()
    {
        if (!(currentCoyoteTime < coyoteTime * coyoteTimeBufferMultiplier && currentCoyoteTime > -1) && hasJumped && canReCheckJump)
        {
            hasJumped = !GroundCheck();
            if (GroundCheck())
            {
                playerAnimator.SetBool("IsJumping", false);
                canReCheckJump = false;
            }
        }

        if (hasJumped)
        {
            currentCoyoteTime = -1f;
            return;
        }      

        if (GroundCheck() && !hasJumped)
        {
            currentCoyoteTime = coyoteTime;
        }
        else if (currentCoyoteTime > 0)
        {
            currentCoyoteTime -= Time.fixedDeltaTime;
        }
    }

    private bool GroundCheck()
    {
        Vector2 leftPosition = new Vector2(transform.position.x - (transform.localScale.x / 2), transform.position.y);
        Vector2 rightPosition = new Vector2(transform.position.x + (transform.localScale.x / 2), transform.position.y);

        bool groundHit;

        groundHit = Physics2D.Raycast(leftPosition, -transform.up, groundCheckDistance, groundLayerMask) || Physics2D.Raycast(rightPosition, -transform.up, groundCheckDistance, groundLayerMask);

        return groundHit;
    }

    public void Dash()
    {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        currentMovementSpeed = dashSpeed;
        soundEffectManager.PlaySound(dashSound);
        playerAnimator.SetBool("IsDashing", true);

        yield return new WaitForSeconds(dashTime);

        currentMovementSpeed = movementSpeed;
        playerAnimator.SetBool("IsDashing", false);

    }

    public void GroundPound()
    {
        if (!GroundCheck())
        {
            groundPounding = true;
            playerRigidbody.AddForce(new Vector2(0, -groundPoundStrength * 1000));          
        }
    }

    private void PerformGroundPound()
    {
        if (!groundPounding) { return; }

        if (GroundCheck())
        {
            playerAnimator.SetBool("IsGroundPounding", true);
            CheckGroundPound();
            groundPounding = false;
            StartCoroutine(WaitWithAnimationTurnoffRoutine());
        }
    }

    private IEnumerator WaitWithAnimationTurnoffRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        playerAnimator.SetBool("IsGroundPounding", false);
    }

    private void CheckGroundPound()
    {
        RaycastHit2D hitLeft;
        RaycastHit2D hitRight;

        hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 4)), Vector2.left, groundPoundCheckLength, enemyLayers);
        hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 4)), Vector2.right, groundPoundCheckLength, enemyLayers);

        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.TryGetComponent(out Enemy leftEnemy))
            {
                leftEnemy.Damage(damageToEnemies);
            }
        }

        if (hitRight.collider != null)
        {
            if (hitRight.collider.TryGetComponent(out Enemy rightEnemy))
            {
                rightEnemy.Damage(damageToEnemies);
            }
        }       
    }

    private void OnDrawGizmosSelected()
    {
        // Ground Check Gizmos

        Gizmos.color = Color.cyan;

        Vector2 playerLeftPosition = new Vector2(transform.position.x - (transform.localScale.x / 2), transform.position.y);
        Vector2 playerRightPosition = new Vector2(transform.position.x + (transform.localScale.x / 2), transform.position.y);

        Vector2 groundPosLeft = new Vector2(transform.position.x - (transform.localScale.x / 2), transform.position.y - groundCheckDistance);
        Vector2 groundPosRight = new Vector2(transform.position.x + (transform.localScale.x / 2), transform.position.y - groundCheckDistance);

        Gizmos.DrawLine(playerLeftPosition, groundPosLeft);
        Gizmos.DrawLine(playerRightPosition, groundPosRight);

        // Ground Pound Gizmos

        Gizmos.color = Color.green;

        Vector2 leftPosition = new Vector2(transform.position.x - groundPoundCheckLength, transform.position.y - (transform.localScale.y / 4));
        Vector2 rightPosition = new Vector2(transform.position.x + groundPoundCheckLength, transform.position.y - (transform.localScale.y / 4));

        Gizmos.DrawLine(leftPosition, rightPosition);
    }
}
