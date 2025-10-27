using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        moveDir = direction;
    }

    public void StopMoving() => moveDir = Vector2.zero;

    public void ResumeMoving() { }

    public bool IsFacingRight() => moveDir.x >= 0;
}
