using UnityEngine;
using TMPro;

public class Enough : MonoBehaviour
{
    [SerializeField] private TMP_Text EnoughText;
    [SerializeField] private string message = "Press C to buy item!";
    [SerializeField] private float showDistance = 2f;

    private Transform player;
    private bool isVisible = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (EnoughText != null)
            EnoughText.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (player == null || EnoughText == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= showDistance && !isVisible && CoinManager.coins >= Shop.price)
            ShowText();
        else if (dist > showDistance && isVisible || CoinManager.coins < Shop.price)
            HideText();
    }

    private void ShowText()
    {
        isVisible = true;
        EnoughText.text = message;
        EnoughText.gameObject.SetActive(true);
    }

    private void HideText()
    {
        isVisible = false;
        EnoughText.gameObject.SetActive(false);
    }
}
