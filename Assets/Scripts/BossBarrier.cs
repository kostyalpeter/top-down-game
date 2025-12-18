using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    private Transform player;
    public GameObject barrier1;
    public GameObject barrier2;
    public GameObject barrier3;
    public GameObject barrier4;
    public GameObject boss;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                barrier1.SetActive(true);
                barrier2.SetActive(true);
                barrier3.SetActive(true);
                barrier4.SetActive(true);
                boss.SetActive(true);}
    }
    
}
