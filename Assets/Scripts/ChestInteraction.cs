using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [Header("Settings")]
    public GameObject closedChest;   // a zárt láda object
    public GameObject openChest;     // a nyitott láda object
    public float interactDistance = 2f;

    private Transform player;
    private bool opened = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (closedChest == null)
            closedChest = gameObject; // ha ez maga a zárt láda

        if (openChest != null)
            openChest.SetActive(false);
    }

    void Update()
    {
        if (opened || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // Ha közel van és megnyomja a C-t
        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        opened = true;

        // Kikapcsoljuk a zárt ládát
        if (closedChest != null)
            closedChest.SetActive(false);

        // Bekapcsoljuk a nyitott ládát
        if (openChest != null)
            openChest.SetActive(true);

        Debug.Log("🗝️ Chest opened!");
    }
}
