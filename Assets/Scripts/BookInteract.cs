using UnityEngine;
using TMPro;

public class BookInteract : MonoBehaviour
{
    [Header("UI elemek")]
    public GameObject bookUI;

    [Header("1. oldal szövegek")]
    public TMP_Text firstPageText1;
    public TMP_Text firstPageText2;

    [Header("2. oldal szövegek")]
    public TMP_Text secondPageText1;
    public TMP_Text secondPageText2;

    [Header("3. oldal szövegek")]
    public TMP_Text thirdPageText1;
    public TMP_Text thirdPageText2;

    [Header("Egyéb beállítások")]
    public Transform player;
    public float interactDistance = 2f;

    private bool isReading = false;
    private int currentPage = 0;
    private int totalPages = 3;

    void Start()
    {
        HideAllPages();
        if (bookUI != null)
            bookUI.SetActive(false);
    }

    void Update()
    {
        if (player == null || bookUI == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= interactDistance && Input.GetKeyDown(KeyCode.C))
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

        if (isReading && (Input.GetKeyDown(KeyCode.Escape) || distanceToPlayer > interactDistance))
        {
            CloseBook();
        }
    }

    private void OpenBook()
    {
        isReading = true;
        currentPage = 1;
        bookUI.SetActive(true);
        ShowPage(currentPage);

        Time.timeScale = 0f;
    }

    private void NextPage()
    {
        currentPage++;

        if (currentPage > totalPages)
        {
            CloseBook();
            return;
        }

        ShowPage(currentPage);
    }

    private void CloseBook()
    {
        isReading = false;
        bookUI.SetActive(false);
        HideAllPages();

        Time.timeScale = 1f;

    }

    private void ShowPage(int page)
    {
        HideAllPages();

        switch (page)
        {
            case 1:
                if (firstPageText1 != null) firstPageText1.gameObject.SetActive(true);
                if (firstPageText2 != null) firstPageText2.gameObject.SetActive(true);
                break;

            case 2:
                if (secondPageText1 != null) secondPageText1.gameObject.SetActive(true);
                if (secondPageText2 != null) secondPageText2.gameObject.SetActive(true);
                break;

            case 3:
                if (thirdPageText1 != null) thirdPageText1.gameObject.SetActive(true);
                if (thirdPageText2 != null) thirdPageText2.gameObject.SetActive(true);
                break;
        }
    }

    private void HideAllPages()
    {
        if (firstPageText1 != null) firstPageText1.gameObject.SetActive(false);
        if (firstPageText2 != null) firstPageText2.gameObject.SetActive(false);
        if (secondPageText1 != null) secondPageText1.gameObject.SetActive(false);
        if (secondPageText2 != null) secondPageText2.gameObject.SetActive(false);
        if (thirdPageText1 != null) thirdPageText1.gameObject.SetActive(false);
        if (thirdPageText2 != null) thirdPageText2.gameObject.SetActive(false);
    }
}
