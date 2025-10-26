using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private bool isBroken = false;

    public GameObject destroyEffectPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isBroken) return;

        currentHealth -= damage;
        Debug.Log(name + " kapott " + damage + " sebzést, HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        if (isBroken) return;
        isBroken = true;

        Debug.Log(name + " széttörik!");

        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);

            ParticleSystem[] particles = effect.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in particles)
            {
                ps.Play();
            }

            float duration = 0f;
            foreach (var ps in particles)
            {
                duration = Mathf.Max(duration, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            Destroy(effect, duration + 0.1f);
        }

        Destroy(gameObject);
    }
}
