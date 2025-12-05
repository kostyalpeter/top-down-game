using UnityEngine;
using TMPro;

public class NotEnoughText : MonoBehaviour
{
    [SerializeField] private TMP_Text NotEnough;
    [SerializeField] private string message = "Not enough coins!";
    [SerializeField] private float showDistance = 2f;

    private Transform player;
    private bool isVisible = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (NotEnough != null)
            NotEnough.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (player == null || NotEnough == null || CoinManager.coins >= Shop.price) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= showDistance && !isVisible)
            ShowText();
        else if (dist > showDistance && isVisible)
            HideText();
    }

    private void ShowText()
    {
        isVisible = true;
        NotEnough.text = message;
        NotEnough.gameObject.SetActive(true);
    }

    private void HideText()
    {
        isVisible = false;
        NotEnough.gameObject.SetActive(false);
    }
}
