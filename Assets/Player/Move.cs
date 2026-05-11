using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    private float moveX;

    public float JumpForce = 10f;

    public LayerMask GroundLayer;

    public BoxCollider2D GroundCollider;

    public bool OnGround;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       OnGround = true;
    }
    void Update()
    {
        //moves left and right
        moveX = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            OnGround = false; 
        } 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(GroundLayer == (1 << other.gameObject.layer))
        {
            OnGround = true;
        }
    }

    void FixedUpdate()
    {
        movement = new Vector2(moveX * moveSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
        rb.linearVelocity = movement;
    }

}