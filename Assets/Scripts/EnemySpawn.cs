using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform player;
    public GameObject Orc;
    public GameObject OrcRed;
    public GameObject OrcBlue;
    public GameObject OrcYellow;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Instantiate(Orc, transform.position + Vector3.right * 2, Quaternion.identity);
            return;
        }
    }
}
