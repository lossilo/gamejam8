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
    [SerializeField] private LayerMask groundLayerMask;

    private bool moveBlock;
    private float currentMovementSpeed;

    private Rigidbody2D playerRigidbody;

    public bool MoveBlock { get { return moveBlock; } set { moveBlock = value; } }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        currentMovementSpeed = movementSpeed;
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
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
        }
        else
        {
            playerRigidbody.linearVelocity = new Vector2(0, playerRigidbody.linearVelocity.y);
        }

        if (Mathf.Abs(playerRigidbody.linearVelocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.linearVelocity.x), transform.localScale.y);
        }
    }

    public void Jump()
    {
        if (!moveBlock && GroundCheck())
        {
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 1000));
        }
    }

    bool GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Dash()
    {
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        currentMovementSpeed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        currentMovementSpeed = movementSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 groundPos = new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z);
        Gizmos.DrawLine(transform.position, groundPos);
    }
}
