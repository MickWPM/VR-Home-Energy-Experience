using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BreakerBoardAnimations : MonoBehaviour
{
    public AnimationCurve doorOpenCurve;
    public float openingTime = 0.5f;
    public float startRot = 0, endRot = -140;
    public Transform doorTransform;

    private float t = 0;

    private bool holdDoorOpen = false;

    //t being passed in these events so we can act on that if required
    //Practically we are using this for the audio source to make sure it finishes when we want it to
    public UnityEvent<float> BreakerDoorOpenAtProgressEvent, BreakerDoorCloseAtProgressEvent;
    [ContextMenu("Open door")]
    public void OpenDoor()
    {
        //This is an external call - we ignore it if we are already holding open
        if (holdDoorOpen) return;

        direction = 1;
        BreakerDoorOpenAtProgressEvent?.Invoke(t);
        if (animating == false)
        {
            AnimateDoor();
        }
    }

    [ContextMenu("Close door")]
    public void CloseDoor()
    {
        //This is an external call - we ignore it if we are already holding open
        if (holdDoorOpen) return;

        direction = -1;
        BreakerDoorCloseAtProgressEvent?.Invoke(t);
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
            if (holdDoorOpen) complete = true;
        }

        float targetRot = direction < 0 ? startRot : endRot;
        doorTransform.localRotation = Quaternion.Euler(new Vector3(0, targetRot, 0));
        AnimationComplete();
    }

    //Tidy up any animation related elements
    //We can also add events here if needed
    private void AnimationComplete()
    {
        animating = false;
        //Edge case for if the hold open button is clicked while animating
        if (holdDoorOpen) SetDoorOpen();
    }

    public Toggle heldOpenToggle;
    public void ToggleDoorHoldOpen()
    {
        holdDoorOpen = !holdDoorOpen;
        heldOpenToggle.isOn = holdDoorOpen;
        //If we are animating, the animation cleanup will set the door to the correct open position if required
        if (animating == false && holdDoorOpen)
        {
            SetDoorOpen();
        }
    }

    private void SetDoorOpen()
    {
        doorTransform.localRotation = Quaternion.Euler(new Vector3(0, endRot, 0));
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
