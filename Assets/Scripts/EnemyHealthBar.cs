using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);

    EnemyHealth enemy;
    Camera cam;

    void Awake()
    {
        enemy = GetComponentInParent<EnemyHealth>();
        cam = Camera.main;
    }

    void Start()
    {
        if (enemy && slider)
        {
            slider.minValue = 0;
            slider.maxValue = enemy.maxHealth;
            slider.value = enemy.maxHealth;
        }
    }

    void LateUpdate()
    {
        if (!enemy || !slider) return;

        slider.value = Mathf.Clamp(enemy.GetCurrentHealth(), 0, enemy.maxHealth);

        if (enemy.GetCurrentHealth() <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (cam == null) cam = Camera.main;
        transform.position = enemy.transform.position + offset;
    }
}
