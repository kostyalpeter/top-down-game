using UnityEngine;

public class PowerItem : MonoBehaviour, IItem
{
    public int healAmount = 10;
    public void Use()
    {
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
        if (health != null)
            health.Heal(healAmount);
    }
}
