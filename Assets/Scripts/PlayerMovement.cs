using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool facingRight;

    private SpriteRenderer sprite;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // mozgás input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // player flip (balra/jobbra)
        if (moveX > 0 && !facingRight)
        {
            facingRight = true;
            sprite.flipX = false;
        }
        else if (moveX < 0 && facingRight)
        {
            facingRight = false;
            sprite.flipX = true;
        }

        // animáció paraméterek
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);
        animator.SetBool("IsMoving", moveInput.magnitude > 0f);
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}
