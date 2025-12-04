using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class Shop : MonoBehaviour
{
    public float interactDistance = 2f;
    private Transform player;
    public int price = 1;
    public GameObject item;

    public void Coin()
    {
        if (CoinManager.coins == price) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("You have enough coins to buy this item!");
        }
    }
}