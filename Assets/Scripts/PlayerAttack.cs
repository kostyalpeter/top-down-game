using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;

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
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("Hit enemy");
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
