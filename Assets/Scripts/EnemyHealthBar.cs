using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider slider;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (enemyHealth == null)
            enemyHealth = GetComponentInParent<EnemyHealth>();

        if (slider != null && enemyHealth != null)
            slider.maxValue = enemyHealth.maxHealth;
    }

    void Update()
    {
        if (enemyHealth == null || slider == null)
            return;

        slider.value = Mathf.Clamp(enemyHealth.CurrentHealth, 0, enemyHealth.maxHealth);
        transform.position = enemyHealth.transform.position + offset;
    }
}
