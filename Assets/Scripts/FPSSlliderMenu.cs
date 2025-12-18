using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSSliderMenu : MonoBehaviour
{
    public int minFPS = 30;
    public int maxFPS = 1000;

    public Slider fpsSlider;
    public TMP_Text fpsText;

    private bool sliderVisible = false;
    private int frames;
    private float timer;
    private int displayedFps;

    void Start()
    {
        if (fpsSlider != null)
        {
            fpsSlider.minValue = minFPS;
            fpsSlider.maxValue = maxFPS;
            fpsSlider.onValueChanged.AddListener(UpdateFPS);
            fpsSlider.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sliderVisible = !sliderVisible;

            if (fpsSlider != null)
                fpsSlider.gameObject.SetActive(sliderVisible);

            Time.timeScale = sliderVisible ? 0f : 1f;
        }

        frames++;
        timer += Time.unscaledDeltaTime;
        if (timer >= 1f)
        {
            displayedFps = Mathf.RoundToInt(frames / timer);
            frames = 0;
            timer = 0f;
        }
    }

    void UpdateFPS(float value)
    {
        QualitySettings.vSyncCount = 0;
        int fps = Mathf.RoundToInt(value);
        Application.targetFrameRate = Mathf.Clamp(fps, minFPS, maxFPS);
    }
}
