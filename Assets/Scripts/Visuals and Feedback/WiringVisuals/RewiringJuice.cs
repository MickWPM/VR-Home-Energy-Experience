using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;

public class RewiringJuice : MonoBehaviour
{
    public WiringVisualiser wiringVisualiser;
    public AudioSource plopSoundSource;
    public GameObject visual;
    public LineRenderer lr;
    private float animationTime;

    [ContextMenu("Show Rewire")]
    private void RewireTest()
    {
        DoRewire();
    }

    public float pitchShiftPerSpawn = 0.05f;
    public float distancePerSecond = 1f;
    public float distancePerSpawn = 0.2f;
    public async void DoRewire()
    {
        //Ignore the initial hookup event for the rewire FX
        if (Time.timeSinceLevelLoad < 1f) return;

        float runningTime = 0;

        //Line renderer get positions is painful. 
        //The GetPositions returns an int which represents the index of the array you passed the function
        //that it filled up to....
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);

        float runningSpawnDistance = 0f;
        float distanceCovered = 0f;
        float toalDistance = GetTotalDistance(positions);
        animationTime = toalDistance / distancePerSecond;

        while (distanceCovered < toalDistance)
        {
            float t = runningTime / animationTime;
            distanceCovered = distancePerSecond * runningTime;

            if (distanceCovered - runningSpawnDistance >= distancePerSpawn)
            {
                Vector3 newPos = GetPositionAtDistance(positions, distanceCovered);
                Instantiate(visual, newPos, Quaternion.identity);
                runningSpawnDistance += distancePerSpawn;
                plopSoundSource.transform.position = newPos;
                plopSoundSource.Play();
                plopSoundSource.pitch *= 1 + pitchShiftPerSpawn;
            }

            await Awaitable.NextFrameAsync();
            runningTime += Time.deltaTime;
        }
        plopSoundSource.pitch = 1;
        plopSoundSource.transform.localPosition = Vector3.zero;
    }

    private float GetTotalDistance(Vector3[] positions)
    {
        float totalDistance = 0f;
        for (int i = 1; positions.Length > i; i++)
        {
            totalDistance += Vector3.Distance(positions[i], positions[i - 1]);
        }
        return totalDistance;
    }

    public Vector3 GetPositionAtDistance(Vector3[] points, float targetDistance)
    {
        float distanceCovered =0f;
        for (int i = 0; i < points.Length-1; i++)
        {
            float legLength = Vector3.Distance(points[i], points[i + 1]);
            if (distanceCovered + legLength >= targetDistance)
            {
                float t = (targetDistance - distanceCovered) / legLength;
                return Vector3.Lerp(points[i], points[i + 1], t);
            }
            distanceCovered += legLength;
        }
        //Failsafe in case we havent reached the target distnace, just return end of the line
        return points[points.Length - 1];
    }

    private void OnEnable()
    {
        wiringVisualiser.CircuitConnectionUpdateCompleteEvent += WiringVisualiser_CircuitConnectionUpdateCompleteEvent;
    }
    private void OnDisable()
    {
        wiringVisualiser.CircuitConnectionUpdateCompleteEvent -= WiringVisualiser_CircuitConnectionUpdateCompleteEvent;
    }

    private void WiringVisualiser_CircuitConnectionUpdateCompleteEvent()
    {
        DoRewire();
    }
}
