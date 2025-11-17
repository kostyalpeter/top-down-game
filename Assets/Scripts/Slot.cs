using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Slot : MonoBehaviour

{
    public void UseItem()
    {
        IItem item = currentItem.GetComponent<IItem>();
        if (item != null)
        {
            item.Use();
            currentItem.GetComponent<Item>().RemoveFromStack(1);
        }
        if (item == null)
        {
            Destroy(currentItem);

        }

    }
    public GameObject currentItem;
}