using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [Header("Settings")]
    public GameObject closedChest;
    public GameObject openChest;
    public float interactDistance = 2f;

    private Transform player;
    private bool opened = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (closedChest == null)
            closedChest = gameObject;

        if (openChest != null)
            openChest.SetActive(false);
    }

    void Update()
    {
        if (opened || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        opened = true;

        if (closedChest != null)
            closedChest.SetActive(false);

        if (openChest != null)
            openChest.SetActive(true);

        Debug.Log("🗝️ Chest opened!");
    }
}
