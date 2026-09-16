using UnityEngine;

public class GlobalVolumeDelay : MonoBehaviour
{
    public float audioStartupDelay = 0.5f;
    private float initialVolume;
    public void Awake()
    {
        initialVolume = AudioListener.volume;
        AudioListener.volume = 0f;
    }
    private void Start()
    {
        ResumeVolume();
    }

    private async void ResumeVolume()
    {
        await Awaitable.WaitForSecondsAsync(audioStartupDelay);
        AudioListener.volume = initialVolume;
    }
}
