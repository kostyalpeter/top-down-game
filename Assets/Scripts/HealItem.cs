using UnityEngine;

public class HealItem : MonoBehaviour, IItem
{
    public int healAmount = 10;
    public GameObject check;
    public void Use()
    {
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
        if (health != null)
            health.Heal(healAmount);
    }
}
