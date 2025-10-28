using UnityEngine;
using TMPro;

public class NPCBubble : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private TMP_Text bubbleText1;
    [SerializeField] private TMP_Text bubbleText2;
    [SerializeField] private float interactDistance = 2f;

    private Transform player;
    private bool isVisible = false;
    private int currentIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        if (bubbleText1 != null)
            bubbleText1.gameObject.SetActive(false);
        if (bubbleText2 != null)
            bubbleText2.gameObject.SetActive(false);

        SetupCanvas();
    }

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

    private void ShowBubble()
    {
        isVisible = true;
        currentIndex = 0;

        bubbleObject.SetActive(true);
        SetupCanvas();

        // Első szöveg mutatása
        if (bubbleText1 != null)
            bubbleText1.gameObject.SetActive(true);
        if (bubbleText2 != null)
            bubbleText2.gameObject.SetActive(false);

        Debug.Log("💬 Bubble Text 1 látszik!");
    }

    private void NextText()
    {
        if (currentIndex == 0)
        {
            if (bubbleText1 != null)
                bubbleText1.gameObject.SetActive(false);
            if (bubbleText2 != null)
                bubbleText2.gameObject.SetActive(true);

            currentIndex = 1;
            Debug.Log("💬 Bubble Text 2 látszik!");
        }
        else
        {
            Debug.Log("🗨️ Marad a második szöveg, nem tűnik el.");
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

        Debug.Log("💭 Buborék és szövegek eltűntek, mert kimentél a hatótávból.");
    }

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
    }
}
