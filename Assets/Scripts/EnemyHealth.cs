using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject boss;

    public int GetCurrentHealth() => currentHealth;

    private bool isDead = false;

    [Header("XP Drop Settings")]
    public GameObject xpOrbPrefab;
    public int minOrbs = 1;
    public int maxOrbs = 3;
    public Vector2 dropOffsetRange = new Vector2(0.5f, 0.5f);
    public Vector2 xpRange = new Vector2(5, 15);


    [Header("Components")]
    private Animator animator;
    private Collider2D col;
    private Rigidbody2D rb;

    public float CurrentHealth { get; internal set; }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (animator != null)
        {
            animator.ResetTrigger("Hurt");
            animator.ResetTrigger("Die");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(name + " sebződött: " + damage + " HP: " + currentHealth);

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
        if (currentHealth <= 0 && boss != null)
        {
            Die();
        }

    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (animator != null)
        {
            animator.ResetTrigger("Hurt");
            animator.SetTrigger("Die");
        }

        if (col != null)
            col.enabled = false;

        if (xpOrbPrefab != null)
        {
            int orbCount = Random.Range(minOrbs, maxOrbs);

            for (int i = 0; i < orbCount; i++)
            {
                Vector2 offset = new Vector2(
                    Random.Range(-dropOffsetRange.x, dropOffsetRange.x),
                    Random.Range(-dropOffsetRange.y, dropOffsetRange.y)
                );

                GameObject orb = Instantiate(xpOrbPrefab, (Vector2)transform.position + offset, Quaternion.identity);

                XPOrb xpOrb = orb.GetComponent<XPOrb>();
                if (xpOrb != null)
                {
                    xpOrb.xpAmount = Mathf.RoundToInt(Random.Range(xpRange.x, xpRange.y));
                }
            }
        }

        Destroy(gameObject, 2f);
    }

    internal bool IsDead()
    {
        throw new System.NotImplementedException();
    }
}
