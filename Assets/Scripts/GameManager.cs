using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text gameOverText;
    public float restartDelay = 5f;
    private bool gameOverTriggered = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);

        Time.timeScale = 0f;

        StartCoroutine(RestartAfterDelay());
    }

    private System.Collections.IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
