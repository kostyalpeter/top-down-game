using System;
using System.Collections;
using System.Data.Common;
using TMPro;
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
    }
    public GameObject currentItem;
}