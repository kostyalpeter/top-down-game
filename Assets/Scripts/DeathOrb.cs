using UnityEngine;

public class DeathOrb : MonoBehaviour
{
    public int damage = 5;
    private Transform player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
