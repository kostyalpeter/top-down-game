using UnityEngine;
using TMPro;

public class BookInteract : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private GameObject bookUI;
    [SerializeField] private GameObject page1Text1;
    [SerializeField] private GameObject page1Text2;
    [SerializeField] private GameObject page2Text1;
    [SerializeField] private GameObject page2Text2;
    [SerializeField] private float interactDistance = 2f;

    private Transform player;
    private bool isReading = false;
    private bool onFirstPage = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bookUI != null)
            bookUI.SetActive(false);

        HideAllText();
    }

    void Update()
    {
        if (player == null || bookUI == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.C))
        {
            if (!isReading)
            {
                OpenBook();
            }
            else
            {
                NextPage();
            }
        }

        if (isReading && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBook();
        }

        if (isReading && dist > interactDistance)
        {
            CloseBook();
        }
    }

    private void OpenBook()
    {
        isReading = true;
        onFirstPage = true;

        if (bookUI != null)
            bookUI.SetActive(true);

        ShowFirstPage();
        Debug.Log("📖 Könyv megnyitva, első oldal látszik!");
    }

    private void NextPage()
    {
        if (onFirstPage)
        {
            ShowSecondPage();
            onFirstPage = false;
        }
        else
        {
            ShowFirstPage();
            onFirstPage = true;
        }
    }

    private void CloseBook()
    {
        isReading = false;
        if (bookUI != null)
            bookUI.SetActive(false);

        HideAllText();
        Debug.Log("📕 Könyv bezárva.");
    }

    private void ShowFirstPage()
    {
        HideAllText();

        if (page1Text1 != null) page1Text1.SetActive(true);
        if (page1Text2 != null) page1Text2.SetActive(true);
    }

    private void ShowSecondPage()
    {
        HideAllText();

        if (page2Text1 != null) page2Text1.SetActive(true);
        if (page2Text2 != null) page2Text2.SetActive(true);
    }

    private void HideAllText()
    {
        if (page1Text1 != null) page1Text1.SetActive(false);
        if (page1Text2 != null) page1Text2.SetActive(false);
        if (page2Text1 != null) page2Text1.SetActive(false);
        if (page2Text2 != null) page2Text2.SetActive(false);
    }
}
