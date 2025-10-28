using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemyLayer;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
                Debug.Log("Enemy sebződött: " + enemy.name);
                continue;
            }

            BreakableObject breakable = hit.GetComponent<BreakableObject>();
            if (breakable != null)
            {
                breakable.TakeDamage(20);
                Debug.Log("Törhető objektum találat: " + breakable.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    private void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
