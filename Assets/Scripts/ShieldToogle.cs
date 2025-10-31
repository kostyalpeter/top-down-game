using UnityEngine;
using System.Collections;

public class PShiledToogle : MonoBehaviour
{
    public GameObject shieldObject;
    public float shieldDuration = 3f;
    public float shieldCooldown = 5f;

    private bool isActive;
    private bool onCooldown;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isActive && !onCooldown)
            StartCoroutine(ActivateShield());
    }

    IEnumerator ActivateShield()
    {
        isActive = true;
        onCooldown = true;

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
        yield return new WaitForSeconds(shieldCooldown);
        onCooldown = false;
    }
}
