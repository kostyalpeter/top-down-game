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
            if (PlayerWeaponManager.Instance == null)
            {
                Debug.LogWarning("Nincs PlayerWeaponManager példány!");
                return;
            }

            var weapon = PlayerWeaponManager.Instance.currentWeapon;

            if (weapon == PlayerWeaponManager.WeaponType.Sword)
            {
                Debug.Log("Kard támadás!");
                animator.SetTrigger("Attack");
            }
            else if (weapon == PlayerWeaponManager.WeaponType.Bow)
            {
                Debug.Log("Íj animáció elindítva!");
                animator.SetTrigger("Attack_Bow");
            }
        }
    }

    public void Attack()
    {
        var wm = PlayerWeaponManager.Instance;
        if (wm == null || wm.currentWeapon != PlayerWeaponManager.WeaponType.Sword) return;

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

    [Header("Bow Settings")]
public GameObject arrowPrefab;
public Transform arrowSpawnPoint;
public float arrowSpeed = 10f;

public void ShootArrow()
{
    var wm = PlayerWeaponManager.Instance;
    if (wm == null || wm.currentWeapon != PlayerWeaponManager.WeaponType.Bow) return;

    if (arrowPrefab == null || arrowSpawnPoint == null)
    {
        Debug.LogWarning("Hiányzik az arrowPrefab vagy arrowSpawnPoint!");
        return;
    }

    GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
    Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
    if (rb != null)
        rb.linearVelocity = arrowSpawnPoint.right * arrowSpeed;

    Debug.Log("🏹 Nyíl kilőve!");
}

}
