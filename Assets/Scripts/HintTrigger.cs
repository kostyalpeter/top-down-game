using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.IO.Compression;
using Unity.Burst.CompilerServices;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] public TMP_Text hintText;
    [SerializeField] private string message = "Press C to interact";
    [SerializeField] private float showDistance = 2f;

    private Transform player;
    private bool isVisible = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

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
    void OnDisable()
    {
        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }
}
