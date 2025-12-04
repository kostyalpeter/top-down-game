using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TMP_Text coinText;
    public GameObject coin;
    public int coins = 0;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinText != null)
            coinText.text = ":" + coins;
            coin.SetActive(true);
    }
}
