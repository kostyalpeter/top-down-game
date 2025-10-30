using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public enum WeaponType { Sword, Bow }
    public WeaponType currentWeapon = WeaponType.Sword;

    void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;
    }
}
