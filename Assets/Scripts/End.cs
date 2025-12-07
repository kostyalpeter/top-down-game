using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject boss;
    public EnemyHealth enemyHealth;
    public void EndGame()
    {
        if (enemyHealth.CurrentHealth <= 0)
        {
            Debug.Log("Game Over! You Win!");
        }
    }

}
