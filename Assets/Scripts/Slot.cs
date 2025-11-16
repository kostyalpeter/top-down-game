using UnityEngine;

public class Slot : MonoBehaviour

{
    public void UseItem()
    {
        IItem item = currentItem.GetComponent<IItem>();
        if (item != null)
        {
            item.Use();
            Destroy(currentItem);
        }
    }
    public GameObject currentItem;
}