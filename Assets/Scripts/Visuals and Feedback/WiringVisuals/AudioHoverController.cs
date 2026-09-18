using System;
using UnityEngine;

//The purpose of this script is to stop the audio clipping on hover end.
//The initial approach of just Play/Stop on Hover/unhover resulted in a distinctive popping sound
//To mitigate this it is best to fade the sound out so we arent stopping at a non zero point in the waveform
//The initial approach was to use aysnc however it is much cleaner (and negligable performance) to just 
//consider a target volume in Update. There is probably a better way but the fade in in particular is actually now a feature
public class AudioHoverController : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime = 1;
    private float baseVolume;
    private float targetVolume;
    private void Awake()
    {
        baseVolume = audioSource.volume;
        audioSource.volume = 0;
        targetVolume = 0;
    }


    [ContextMenu("Hover start")]
    public void HoverStart()
    {
        targetVolume = baseVolume;
        if (audioSource.isPlaying == false)
        {
            audioSource.volume = 0;
            audioSource.Play();
        }
    }

    [ContextMenu("Hover end")]
    public void HoverEnd()
    {
        targetVolume = 0;
    }

    private void Update()
    {
        if (audioSource.isPlaying == false) return;
        if (Mathf.Approximately(audioSource.volume, targetVolume)) return;
        float speed = baseVolume / fadeOutTime;
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, speed * Time.deltaTime);

        if (audioSource.volume == 0f && targetVolume == 0f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}