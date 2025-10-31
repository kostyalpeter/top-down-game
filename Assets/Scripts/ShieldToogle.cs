using UnityEngine;

public class ShieldToggle : MonoBehaviour
{
    public GameObject shieldObject;
    public float activeDuration = 3f;
    public float cooldownTime = 5f;
    public PlayerHealth playerHealth;
    public int[] enemyLayersToIgnore;

    private bool isActive;
    private bool isOnCooldown;
    private float activeTimer;
    private float cooldownTimer;
    private int playerLayer;

    void Start()
    {
        if (playerHealth == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerHealth = p.GetComponent<PlayerHealth>();
        }

        if (playerHealth != null)
            playerLayer = playerHealth.gameObject.layer;

        if (shieldObject != null)
            shieldObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOnCooldown)
            ToggleShield(true);

        if (isActive)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
            {
                ToggleShield(false);
                StartCooldown();
            }
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                isOnCooldown = false;
        }
    }

    void ToggleShield(bool state)
    {
        isActive = state;

        if (shieldObject != null)
            shieldObject.SetActive(state);

        if (playerHealth != null)
            playerHealth.SetInvincible(state);

        if (enemyLayersToIgnore != null && playerHealth != null)
        {
            for (int i = 0; i < enemyLayersToIgnore.Length; i++)
                Physics2D.IgnoreLayerCollision(playerLayer, enemyLayersToIgnore[i], state);
        }

        if (state)
            activeTimer = activeDuration;
    }

    void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
    }
}
