using UnityEngine;

public class DeathOrb : MonoBehaviour
{
    public int OrbDamage = 5;
    private Transform player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(OrbDamage);
            Destroy(gameObject);
        }
    }
}
