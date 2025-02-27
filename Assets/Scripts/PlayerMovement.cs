using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hizi
    public Rigidbody2D rb;
    private Vector2 movement;

    void Update()
    {
        // A D W S girislerini alma 
        movement.x = Input.GetAxisRaw("Horizontal"); // A-D / Sol-Sag ok tuslari
        movement.y = Input.GetAxisRaw("Vertical");   // W-S / Yukari-Asagi ok tuslari
    }

    void FixedUpdate()
    {
        // Hareketi uygula
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
