using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemyLayer;

    [Header("Bow Settings")]
    public GameObject arrowPrefab;
    public float arrowSpeed = 10f;
    public float bowFireDelay = 0.45f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var wm = PlayerWeaponManager.Instance;
            if (wm == null) return;

            if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Sword)
            {
                animator.SetTrigger("Attack");
            }
            else if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Bow)
            {
                animator.SetTrigger("Attack_Bow");
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ShootArrow();
        }
    }

    IEnumerator FireAfterDelay(float t)
    {
        yield return new WaitForSeconds(t);
        ShootArrow();
    }

    public void Attack()
    {
        var wm = PlayerWeaponManager.Instance;
        if (wm == null || wm.currentWeapon != PlayerWeaponManager.WeaponType.Sword)
            return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
                continue;
            }

            BreakableObject breakable = hit.GetComponent<BreakableObject>();
            if (breakable != null)
            {
                breakable.TakeDamage(20);
            }
        }
    }

    public void ShootArrow()
    {
        if (arrowPrefab == null) return;

        bool facingLeft = transform.localScale.x < 0f;

        Vector3 spawnPos = transform.position + new Vector3(facingLeft ? -1.0f : 1.0f, 0f, 0f);
        Quaternion spawnRot = Quaternion.identity;

        GameObject arrow = Instantiate(arrowPrefab, spawnPos, spawnRot);

        if (facingLeft)
        {
            Vector3 scale = arrow.transform.localScale;
            scale.x *= -1;
            arrow.transform.localScale = scale;
        }

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = facingLeft ? Vector2.left : Vector2.right;
            rb.linearVelocity = dir * arrowSpeed;
        }
    }



    private void OnDrawGizmos()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
