using UnityEngine;
using TMPro;

public class FPSDebug: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float updateInterval = 0.5f;

    private float accumulatedTime = 0f;
    private int frameCount = 0;
    private bool showFPS = false;

    private void Awake()
    {
        fpsText.gameObject.SetActive(showFPS); 
    }

    public void ToggleFPSDisplay()
    {
        showFPS = !showFPS;
        fpsText.gameObject.SetActive(showFPS);
    }

    void Update()
    {
        if (showFPS == false) return;

        accumulatedTime += Time.unscaledDeltaTime;
        frameCount++;

        if (accumulatedTime >= updateInterval)
        {
            float fps = frameCount / accumulatedTime;
            fpsText.text = $"{Mathf.RoundToInt(fps)} FPS";

            accumulatedTime = 0f;
            frameCount = 0;
        }
    }
}