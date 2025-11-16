using UnityEngine;

public class XPItem : MonoBehaviour, IItem
{
    public int xpAmount = 10;
    public void Use()
    {
            PlayerXP xp = FindFirstObjectByType<PlayerXP>();
            if (xp != null)
                xp.AddXP(xpAmount);
    }
}
