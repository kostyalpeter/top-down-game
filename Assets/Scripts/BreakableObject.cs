using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private bool isBroken = false;

    public GameObject destroyEffect;

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

        if (destroyEffect != null)
            Instantiate(destroyEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, 0f);
    }
}
