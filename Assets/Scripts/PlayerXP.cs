using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Beállítások")]
    public int currentXP = 0;
    public int currentLevel = 1;
    public int baseXPToNextLevel = 100;
    public float xpGrowth = 1.5f;

    [Header("UI elemek")]
    public Slider xpBar;
    public TMP_Text levelText;

    private int xpToNextLevel;

    void Start()
    {
        xpToNextLevel = baseXPToNextLevel;
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        currentLevel++;
        xpToNextLevel = Mathf.RoundToInt(baseXPToNextLevel * Mathf.Pow(xpGrowth, currentLevel - 1));
    }

    void UpdateUI()
    {
        if (xpBar != null)
            xpBar.value = (float)currentXP / xpToNextLevel;

        if (levelText != null)
            levelText.text = "Lvl " + currentLevel;
    }
}
