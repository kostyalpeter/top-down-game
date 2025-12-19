using Unity.Mathematics;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public float time = 180;
    public float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Spawn();
            timer = 0f;
        }
    }
    void Spawn()
    {
        Instantiate(Enemy, transform.position + Vector3.left *2, Quaternion.identity);
    }
}
