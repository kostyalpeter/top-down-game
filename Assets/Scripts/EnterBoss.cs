using UnityEngine;

public class EnterBoss : MonoBehaviour
{
    private Transform player;
    public float interactDistance = 2f;
    public GameObject rocks;
    public GameObject barriers;
    public GameObject target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            DeleteRocks();
        }
    }

    void DeleteRocks()
    {
        rocks.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            barriers.SetActive(true);
        }
    }
}
