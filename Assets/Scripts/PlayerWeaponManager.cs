using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public enum WeaponType { Sword, Bow, Magic }
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
