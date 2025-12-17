using UnityEngine;

public class Shop : MonoBehaviour
{
    public float interactDistance = 2f;
    public static int price = 50;
    public GameObject item;
    Transform player;
    NotEnoughText NotEnoughText;
    HintTrigger hint;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            if (CoinManager.coins >= price)
            {
                CoinManager.coins -= price;
                CoinManager.Instance.UpdateUI();
                InventoryContoller.Instance.AddItem(item);
            }
        }
    }
}
