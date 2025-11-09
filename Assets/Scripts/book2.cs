using UnityEngine;
using TMPro;

public class book2 : MonoBehaviour
{
    public GameObject bookUI;

    public TMP_Text firstPageText1;
    public TMP_Text firstPageText2;

    public TMP_Text secondPageText1;
    public TMP_Text secondPageText2;

    public Transform player;
    public float interactDistance = 2f;

    private bool isReading = false;
    private int currentPage = 0;
    private int totalPages = 2;

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
        }
    }

    private void HideAllPages()
    {
        if (firstPageText1 != null) firstPageText1.gameObject.SetActive(false);
        if (firstPageText2 != null) firstPageText2.gameObject.SetActive(false);
        if (secondPageText1 != null) secondPageText1.gameObject.SetActive(false);
        if (secondPageText2 != null) secondPageText2.gameObject.SetActive(false);
    }
}
