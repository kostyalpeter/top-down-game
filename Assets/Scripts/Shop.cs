using UnityEngine;

public class Shop : MonoBehaviour
{
    public CoinManager refscrpit;

    [System.Obsolete]
    void Start()
    {
        if (int = refscrpit.coins.) return;
        Debug.Log("Not enough coins to open shop.");

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Purchased item!");
        }
    }
    }