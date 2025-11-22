using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemyLayer;

    public GameObject arrowPrefab;
    public float arrowSpeed = 10f;
    public float attackCooldown = 0.6f;

    public GameObject fireballPrefab;
    public float FireBallSpeed = 10f;
    public float MagicAttackCooldown = 0.6f;


    private bool canAttack = true;
    private bool takingDamage = false;

    void Update()
    {
        if (takingDamage) return;
        if (!canAttack) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            var wm = PlayerWeaponManager.Instance;
            if (wm == null) return;

            if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Sword)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    animator.ResetTrigger("Attack_Bow");
                    animator.SetTrigger("Attack");
                    StartCoroutine(AttackCooldown());
                }
            }
            else if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Bow)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack_Bow");
                    StartCoroutine(AttackCooldown());
                }
            }
            else if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Magic)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack_Bow");
                    StartCoroutine(AttackCooldown());
                }
            }
        }
    }

    public void SetTakingDamage(bool state)
    {
        takingDamage = state;
        if (state)
        {
            StopAllCoroutines();
            canAttack = false;
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Attack_Bow");
        }
        else
        {
            StartCoroutine(ResetAttackAfterDamage());
        }
    }

    IEnumerator ResetAttackAfterDamage()
    {
        yield return new WaitForSeconds(0.1f);
        canAttack = true;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
        var wm = PlayerWeaponManager.Instance;
        if (wm == null) return;

        if (wm.currentWeapon == PlayerWeaponManager.WeaponType.Magic)
        {
            ShootMagic();
            return;
        }

        if (wm.currentWeapon != PlayerWeaponManager.WeaponType.Bow) return;
        if (fireballPrefab == null) return;

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

    public void ShootMagic()
    {
        var wm = PlayerWeaponManager.Instance;
        if (wm == null) return;
        if (wm.currentWeapon != PlayerWeaponManager.WeaponType.Magic) return;
        if (fireballPrefab == null) return;

        bool facingLeft = transform.localScale.x < 0f;
        Vector3 spawnPos = transform.position + new Vector3(facingLeft ? -1.0f : 1.0f, 0f, 0f);
        Quaternion spawnRot = Quaternion.identity;

        GameObject FireBall = Instantiate(fireballPrefab, spawnPos, spawnRot);

        if (facingLeft)
        {
            Vector3 scale = FireBall.transform.localScale;
            scale.x *= -1;
            FireBall.transform.localScale = scale;
        }

        Rigidbody2D rb = FireBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = facingLeft ? Vector2.left : Vector2.right;
            rb.linearVelocity = dir * FireBallSpeed;
        }
    }


    void OnDrawGizmos()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
