using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float radius;
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(20);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    private void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }
}
