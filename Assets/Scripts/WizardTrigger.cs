using UnityEngine;
using TMPro;

public class WizardTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private TMP_Text[] bubbleTexts;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] public GameObject Magic;
    private Transform player;
    private bool isVisible = false;
    private int currentTextIndex = 0;
    private bool rocksRemoved = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        foreach (TMP_Text text in bubbleTexts)
            text.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null || bubbleObject == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            if (!isVisible)
                ShowBubble();
            else
                NextText();
        }

        if (isVisible && dist > interactDistance)
            HideBubble();
    }

    private void ShowBubble()
    {
        isVisible = true;
        currentTextIndex = 0;
        bubbleObject.SetActive(true);
        bubbleTexts[currentTextIndex].gameObject.SetActive(true);
    }

    private void NextText()
    {
        bubbleTexts[currentTextIndex].gameObject.SetActive(false);
        currentTextIndex++;

        if (currentTextIndex >= bubbleTexts.Length)
        {
            HideBubble();

            {
                Magic.SetActive(true);
            }

            return;
        }

        bubbleTexts[currentTextIndex].gameObject.SetActive(true);
    }

    private void HideBubble()
    {
        isVisible = false;
        bubbleObject.SetActive(false);

        foreach (TMP_Text text in bubbleTexts)
            text.gameObject.SetActive(false);
    }
}