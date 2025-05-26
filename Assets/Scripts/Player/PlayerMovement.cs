using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private bool moveBlock;

    private Rigidbody2D playerRigidbody;

    public bool MoveBlock { get { return moveBlock; } set { moveBlock = value; } }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        float inputFloat = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(inputFloat) > Mathf.Epsilon)
        {
            playerRigidbody.linearVelocity = new Vector2(inputFloat * movementSpeed, playerRigidbody.linearVelocity.y);
        }
        else
        {
            playerRigidbody.linearVelocity = new Vector2(0, playerRigidbody.linearVelocity.y);
        }
    }
}
