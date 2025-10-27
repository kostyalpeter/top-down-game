using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;

    private void Start()
    {
        if (!playerHealth) playerHealth = FindObjectOfType<PlayerHealth>();
        if (!slider) slider = GetComponent<Slider>();

        slider.minValue = 0;
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.maxHealth;
    }

    private void Update()
    {
        if (!playerHealth) return;

        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.CurrentHealth;

        if (slider.fillRect)
            slider.fillRect.gameObject.SetActive(playerHealth.CurrentHealth > 0);
    }
}
