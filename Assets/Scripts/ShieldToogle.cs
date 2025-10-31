using UnityEngine;
using System.Collections;

public class ShieldToggle : MonoBehaviour
{
    [Header("Shield Settings")]
    public GameObject shieldObject;
    public PlayerHealth playerHealth;
    public float shieldDuration = 3f;
    public float shieldCooldown = 5f;

    private bool isActive;
    private bool onCooldown;

    void Start()
    {
        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && !isActive && !onCooldown)
        {
            StartCoroutine(ActivateShield());
        }
    }

    private IEnumerator ActivateShield()
    {
        isActive = true;
        onCooldown = true;

        Debug.Log("🛡️ Pajzs aktiválva!");

        if (shieldObject != null)
            shieldObject.SetActive(true);

        if (playerHealth != null)
            playerHealth.SetInvincibleState(true);

        yield return new WaitForSeconds(shieldDuration);

        if (shieldObject != null)
            shieldObject.SetActive(false);

        if (playerHealth != null)
            playerHealth.SetInvincibleState(false);

        isActive = false;
        Debug.Log("🛡️ Pajzs lejárt!");

        yield return new WaitForSeconds(shieldCooldown);
        onCooldown = false;
    }
}
