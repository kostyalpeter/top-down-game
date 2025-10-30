using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;
    public int maxFPS = 67;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        int clampedFPS = Mathf.Min(Mathf.RoundToInt(fps), maxFPS);
        fpsText.text = clampedFPS + " FPS";
    }
}
