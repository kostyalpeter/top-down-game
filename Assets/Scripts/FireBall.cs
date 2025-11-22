using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 150;
    public float lifeTime = 5f;
    public float destroyDelay = 0.05f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject, destroyDelay);
            return;
        }

        BreakableObject breakable = other.GetComponent<BreakableObject>();
        if (breakable != null)
        {
            breakable.TakeDamage(damage);
            Destroy(gameObject, destroyDelay);
            return;
        }

        Destroy(gameObject, destroyDelay);
    }
}
