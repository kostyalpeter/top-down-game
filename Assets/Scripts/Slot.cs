using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject currentItem;

    public void UseItem()
    {
        if (currentItem == null) return;

        IItem itemInterface = currentItem.GetComponent<IItem>();
        Item itemData = currentItem.GetComponent<Item>();

        if (itemInterface != null)
        {
            itemInterface.Use();
        }

        if (itemData != null)
        {
            itemData.RemoveFromStack(1);
            if (itemData.quantity <= 0)
            {
                Destroy(currentItem);
                currentItem = null;
            }
        }
        else
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }
}
