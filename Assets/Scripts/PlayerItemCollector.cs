using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryContoller inventoryController;
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryContoller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if(itemAdded)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
