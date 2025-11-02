using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
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

    public void TriggerGameOver()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);

    }

}
