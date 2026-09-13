using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BreakerBoardAnimations : MonoBehaviour
{
    public AnimationCurve doorOpenCurve;
    public float openingTime = 0.5f;
    public float startRot = 0, endRot = -140;
    public Transform doorTransform;

    private float t = 0;
    

    [ContextMenu("Open door")]
    public void OpenDoor()
    {
        direction = 1;

        if (animating == false)
        {
            AnimateDoor();
        }
    }

    [ContextMenu("Close door")]
    public void CloseDoor()
    {
        direction = -1;

        if (animating == false)
        {
            AnimateDoor();
        } 
    }

    private int direction = 1;
    private bool animating = false;
    //Using this approach to animate the door as:
    //1. Animation Clips are less flexible - we can easily math update here
    //2. It is trivial to make an open and a close method (left commented below)
    //....but if we want to change the direction halfway, it becomes complex
    //3. This just lets us animate till we reach a stopping point
    private async void AnimateDoor()
    {
        animating = true;
        bool complete = false;
        float anim_t;
        float currentRot;
        int targetT = direction < 0 ? 0 : 1;
        while (!complete)
        {
            anim_t = doorOpenCurve.Evaluate(t);
            currentRot = Mathf.LerpUnclamped(startRot, endRot, anim_t);
            doorTransform.localRotation = Quaternion.Euler(new Vector3(0, currentRot, 0));
            await Awaitable.EndOfFrameAsync();
            t += direction * Time.deltaTime / openingTime;

            if (t >= 1f || t <= 0f)
            {
                t = Mathf.Clamp01(t); 
                complete = true;
            }
        }

        float targetRot = direction < 0 ? startRot : endRot;
        doorTransform.localRotation = Quaternion.Euler(new Vector3(0, targetRot, 0));
        animating = false;
    }


    //[ContextMenu("Open door")]
    //public void OpenDoor()
    //{
    //    OpenDoorAnimation();
    //}
    //private async void OpenDoorAnimation()
    //{
    //    t = 0;
    //    float anim_t = 0;
    //    float currentRot = startRot;
    //    while (t<1)
    //    {
    //        anim_t = doorOpenCurve.Evaluate(t);
    //        currentRot = Mathf.Lerp(startRot, endRot, anim_t);
    //        doorTransform.localRotation = Quaternion.Euler(new Vector3(0, currentRot, 0));
    //        await Awaitable.EndOfFrameAsync();
    //        t += Time.deltaTime / openingTime;
    //    }
    //    doorTransform.localRotation = Quaternion.Euler(new Vector3(0, endRot, 0));
    //}

    //[ContextMenu("Close door")]
    //public void CloseDoor()
    //{
    //    CloseDoorAnimation();
    //}

    //private async void CloseDoorAnimation()
    //{
    //    float t = 1;
    //    float anim_t = 0;
    //    float currentRot = startRot;
    //    while (t > 0)
    //    {
    //        anim_t = doorOpenCurve.Evaluate(t);
    //        currentRot = Mathf.Lerp(startRot, endRot, anim_t);
    //        doorTransform.localRotation = Quaternion.Euler(new Vector3(0, currentRot, 0));
    //        await Awaitable.EndOfFrameAsync();
    //        t -= Time.deltaTime / openingTime;
    //    }
    //    doorTransform.localRotation = Quaternion.Euler(new Vector3(0, startRot, 0));
    //}
}
