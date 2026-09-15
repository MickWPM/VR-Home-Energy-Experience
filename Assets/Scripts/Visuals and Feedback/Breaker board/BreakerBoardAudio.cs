using UnityEngine;
using UnityEngine.Audio;

public class BreakerBoardAudio : MonoBehaviour
{
    public AudioSource doorSource, mainsSwitch, mainsPowerLost, mainsPowerReturned;

    public void MainsPowerUpdate(bool powered)
    {
        if (!powered) mainsPowerLost.Play();
        else
        {
            mainsSwitch.Play();
            mainsPowerReturned.Play();
        }
    }

    public void MainsSwitchOn()
    {
    }

    public void DoorOpening(float t)
    {
        float newTime = t * doorSource.clip.length;
        doorSource.Play();
        doorSource.time = t * doorSource.clip.length;
    }
    public void DoorClosing(float t)
    {
        t = 1 - t;
        float newTime = t * doorSource.clip.length;
        doorSource.Play();
        doorSource.time = newTime;
    }
}