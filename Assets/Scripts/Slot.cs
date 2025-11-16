using UnityEngine;

public class Slot : MonoBehaviour

{

    public void UseItem()
    {
        IItem item = currentItem.GetComponent<IItem>();
        if (item != null)
        {
            item.Use();
            currentItem.GetComponent<Item>().quantity -= 1;
        }
    }
    public GameObject currentItem;
}