using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponIconClick : MonoBehaviour, IPointerClickHandler
{
    public PlayerWeaponManager.WeaponType weaponType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerWeaponManager.Instance != null)
            PlayerWeaponManager.Instance.EquipWeapon(weaponType);
    }
}
