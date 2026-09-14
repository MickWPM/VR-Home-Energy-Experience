using Unity.XR.CoreUtils;
using UnityEngine;

public class FloorplanTeleporter : MonoBehaviour
{
    public XROrigin origin;
    public Transform landryPos, kitchenPos, diningPos, breakerBox;

    [ContextMenu("Teleport to laundry")]
    public void TeleportToLaundry()
    {
        TeleportToTransform(landryPos);
    }
    [ContextMenu("Teleport to kitchen")]
    public void TeleportToKitchen()
    {
        TeleportToTransform(kitchenPos);
    }
    [ContextMenu("Teleport to dining")]
    public void TeleportToDining()
    {
        TeleportToTransform(diningPos);
    }
    [ContextMenu("Teleport to breaker")]
    public void TeleportToBreaker()
    {
        TeleportToTransform(breakerBox);
    }

    //Pre and post teleport events (script only) for functionality as required
    //passes targetPos
    public event System.Action<Vector3> BeforeTeleportEvent, AfterTeleportEvent;
    public void TeleportToTransform(Transform targetPos)
    {
        BeforeTeleportEvent?.Invoke(targetPos.position);
     
        origin.MoveCameraToWorldLocation(targetPos.position + Vector3.up * origin.CameraInOriginSpaceHeight);
        origin.MatchOriginUpCameraForward(Vector3.up, targetPos.rotation * Vector3.forward);
        
        AfterTeleportEvent?.Invoke(targetPos.position);
    }

}
