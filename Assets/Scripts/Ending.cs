using UnityEngine;

public class Ending : MonoBehaviour
{
    public float timer = 3f;
    public float time = 0f;
    EnemyHealth enemyHealth;

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer <= time)
        {
            enemyHealth.WinText.SetActive(true);
        }

    }
}
