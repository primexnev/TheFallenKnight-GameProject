using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    private Rigidbody2D rb;
    private Animator animator;
    private float moveInputX;
    private float moveInputY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Get player input for both X and Y axes
        moveInputX = Input.GetAxisRaw("Horizontal"); // A-D
        moveInputY = Input.GetAxisRaw("Vertical");   // W-S

        // Debug log to check if input is detected
        Debug.Log("Move Input X: " + moveInputX + " | Move Input Y: " + moveInputY);

        // Apply movement directly to Rigidbody2D
        rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);

        // Send movement data to Animator
        animator.SetFloat("speed", Mathf.Abs(moveInputX) + Mathf.Abs(moveInputY));

        // Flip character's direction without shrinking
        if (moveInputX > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInputX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
