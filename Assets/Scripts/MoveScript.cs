using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal Movement
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Ground Check (creates a small circle at player's feet)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void FixedUpdate()
    {
        // Apply Movement
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }
}
