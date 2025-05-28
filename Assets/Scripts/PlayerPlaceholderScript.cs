using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create movement vector
        Vector2 movement = new Vector2(moveX, moveY);

        // Apply movement
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
