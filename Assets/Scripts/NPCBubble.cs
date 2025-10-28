using UnityEngine;
using TMPro;

public class NPCBubble : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private GameObject bubbleObject;   // Ide húzd be a TextBubble GameObjectet
    [SerializeField] public TMP_Text bubbleText;       // Ide húzd be a TMP Text komponenst
    [SerializeField] private float interactDistance = 2f;

    private Transform player;
    private bool isVisible = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        SetupCanvas();
    }

    void Update()
    {
        if (player == null || bubbleObject == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            ToggleBubble();
        }

        if (isVisible && dist > interactDistance)
        {
            HideBubble();
        }
    }

    private void ToggleBubble()
    {
        isVisible = !isVisible;

        if (isVisible)
            ShowBubble();
        else
            HideBubble();
    }

    private void ShowBubble()
    {
        if (bubbleObject == null) return;

        bubbleObject.SetActive(true);
        SetupCanvas();

        if (bubbleText != null)
            bubbleText.gameObject.SetActive(true);

        Debug.Log("💬 Buborék megjelent!");
    }

    private void HideBubble()
    {
        if (bubbleObject == null) return;

        bubbleObject.SetActive(false);
        isVisible = false;
        bubbleText.gameObject.SetActive(false);

        Debug.Log("💭 Buborék eltűnt.");
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

        if (bubbleText != null)
        {
            bubbleText.alignment = TextAlignmentOptions.Center;
            bubbleText.enableWordWrapping = true;
            bubbleText.overflowMode = TextOverflowModes.Overflow;
            bubbleText.alpha = 1f;
        }
    }
}
