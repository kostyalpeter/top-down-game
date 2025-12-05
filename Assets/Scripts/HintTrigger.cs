using UnityEngine;
using TMPro;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] public TMP_Text hintText;
    [SerializeField] private string message = "Press C to interact";
    [SerializeField] private float showDistance = 2f;

    private Transform player;
    private bool isVisible = false;
    NotEnoughText notEnoughText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null || hintText == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= showDistance && !isVisible)
            ShowHint();
        else if (dist > showDistance && isVisible)
            HideHint();
    }

    public void ShowHint()
    {
        isVisible = true;
        hintText.text = message;
        hintText.gameObject.SetActive(true);
    }

    public void HideHint()
    {
        isVisible = false;
        hintText.gameObject.SetActive(false);
    }
}
