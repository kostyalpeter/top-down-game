using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float Distance = 10f;
    public GameObject[] enemies;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= Distance)
            ShowEnemy();
    }
    void ShowEnemy()
    {
        foreach (GameObject enemy in enemies)
        {
                enemy.SetActive(true);
        }
    }
}
