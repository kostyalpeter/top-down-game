using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class Shop : MonoBehaviour
{
    public float interactDistance = 2f;
    private Transform player;
    public static int price = 1;
    public GameObject item;

    public void Coin()
    {
        Debug.Log("asd");
        // float dist = Vector2.Distance(transform.position, player.position);
        // if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        // {
        //     Destroy(item);
        // }
    }

}