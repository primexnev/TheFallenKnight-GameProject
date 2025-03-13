using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput; // Stores movement input as a vector

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer for flipping
    }

    private void Update()
    {
        // Handle movement input (Legacy Input System)
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized; // Normalize input to prevent faster diagonal movement

        // Flip character without scaling
        if (moveInput.x > 0)
            spriteRenderer.flipX = false;
        else if (moveInput.x < 0)
            spriteRenderer.flipX = true;

        // Set animation speed
        animator.SetFloat("speed", moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Rigidbody2D for smooth physics-based movement
        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
    }
}
