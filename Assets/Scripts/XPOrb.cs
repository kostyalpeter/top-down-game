using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 10;
    public float moveSpeed = 3f;
    public float pickupRange = 1.5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= pickupRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerXP xp = other.GetComponent<PlayerXP>();
            if (xp != null)
                xp.AddXP(xpAmount);

            Destroy(gameObject);
        }
    }
}
