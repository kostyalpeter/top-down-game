using UnityEngine;
using TMPro;

public class WizardNPC : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private TMP_Text bubbleText1;
    [SerializeField] private TMP_Text bubbleText2;
    [SerializeField] private TMP_Text bubbleText3;
    [SerializeField] private TMP_Text bubbleText4;
    [SerializeField] private TMP_Text bubbleText5;
    [SerializeField] private TMP_Text bubbleText6;
    [SerializeField] private float interactDistance = 2f;

    private Transform player;
    private bool isVisible = false;
    private int currentIndex = 0;

    [System.Obsolete]
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        if (bubbleText1 != null)
            bubbleText1.gameObject.SetActive(false);
        if (bubbleText2 != null)
            bubbleText2.gameObject.SetActive(false);
        if (bubbleText3 != null)
            bubbleText3.gameObject.SetActive(false);
        if (bubbleText4 != null)
            bubbleText4.gameObject.SetActive(false);
        if (bubbleText5 != null)
            bubbleText5.gameObject.SetActive(false);
        if (bubbleText6 != null)
            bubbleText6.gameObject.SetActive(false);
        SetupCanvas();
    }

    [System.Obsolete]
    void Update()
    {
        if (player == null || bubbleObject == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            if (!isVisible)
            {
                ShowBubble();
            }
            else
            {
                NextText();
            }
        }

        if (isVisible && dist > interactDistance)
        {
            HideBubbleAndTexts();
        }
    }

    [System.Obsolete]
    private void ShowBubble()
    {
        isVisible = true;
        currentIndex = 0;

        bubbleObject.SetActive(true);
        SetupCanvas();

        if (bubbleText1 != null)
            bubbleText1.gameObject.SetActive(true);
        if (bubbleText2 != null)
            bubbleText2.gameObject.SetActive(false);
        if (bubbleText3 != null)
            bubbleText3.gameObject.SetActive(false);
        if (bubbleText4 != null)
            bubbleText4.gameObject.SetActive(false);
        if (bubbleText5 != null)
            bubbleText5.gameObject.SetActive(false);
        if (bubbleText6 != null)
            bubbleText6.gameObject.SetActive(false);

    }

    private void NextText()
    {
        if (currentIndex == 0)
        {
            if (bubbleText1 != null)
                bubbleText1.gameObject.SetActive(false);
            if (bubbleText2 != null)
                bubbleText2.gameObject.SetActive(true);
            if (bubbleText3 != null)
                bubbleText3.gameObject.SetActive(false);
            if (bubbleText4 != null)
                bubbleText4.gameObject.SetActive(false);
            if (bubbleText5 != null)
                bubbleText5.gameObject.SetActive(false);
            if (bubbleText6 != null)
                bubbleText6.gameObject.SetActive(false);
            currentIndex = 1;
        }
        else
        {
        }
    }

    private void HideBubbleAndTexts()
    {
        isVisible = false;
        currentIndex = 0;

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        if (bubbleText1 != null)
            bubbleText1.gameObject.SetActive(false);
        if (bubbleText2 != null)
            bubbleText2.gameObject.SetActive(false);
        if (bubbleText3 != null)
            bubbleText3.gameObject.SetActive(false);
        if (bubbleText4 != null)
            bubbleText4.gameObject.SetActive(false);
        if (bubbleText5 != null)
            bubbleText5.gameObject.SetActive(false);
        if (bubbleText6 != null)
            bubbleText6.gameObject.SetActive(false);
    }

    [System.Obsolete]
    private void SetupCanvas()
    {
        Canvas canvas = bubbleObject.GetComponentInChildren<Canvas>(true);
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingLayerName = "UI";
            canvas.sortingOrder = 100;
            canvas.transform.localScale = Vector3.one * 0.01f;
            canvas.transform.localPosition = new Vector3(0, 1.5f, 0);
        }

        if (bubbleText1 != null)
        {
            bubbleText1.alignment = TextAlignmentOptions.Center;
            bubbleText1.enableWordWrapping = true;
            bubbleText1.overflowMode = TextOverflowModes.Overflow;
        }

        if (bubbleText2 != null)
        {
            bubbleText2.alignment = TextAlignmentOptions.Center;
            bubbleText2.enableWordWrapping = true;
            bubbleText2.overflowMode = TextOverflowModes.Overflow;
        }
        if (bubbleText3 != null)
        {
            bubbleText3.alignment = TextAlignmentOptions.Center;
            bubbleText3.enableWordWrapping = true;
            bubbleText3.overflowMode = TextOverflowModes.Overflow;
        }
        if (bubbleText4 != null)
        {
            bubbleText4.alignment = TextAlignmentOptions.Center;
            bubbleText4.enableWordWrapping = true;
            bubbleText4.overflowMode = TextOverflowModes.Overflow;
        }
        if (bubbleText5 != null)
        {
            bubbleText5.alignment = TextAlignmentOptions.Center;
            bubbleText5.enableWordWrapping = true;
            bubbleText5.overflowMode = TextOverflowModes.Overflow;
        }
        if (bubbleText6 != null)
        {
            bubbleText6.alignment = TextAlignmentOptions.Center;
            bubbleText6.enableWordWrapping = true;
            bubbleText6.overflowMode = TextOverflowModes.Overflow;
        }

    }
}
