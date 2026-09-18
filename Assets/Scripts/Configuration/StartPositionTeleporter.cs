using Unity.XR.CoreUtils;
using UnityEngine;

public class StartPositionTeleporter : MonoBehaviour
{
    public XROrigin origin;
    public Transform teleportTargetPos;

    private void Start()
    {
        //Initial space setup in case we moved around in the main scene first
        origin.MoveCameraToWorldLocation(teleportTargetPos.position + Vector3.up * origin.CameraInOriginSpaceHeight);
        origin.MatchOriginUpCameraForward(Vector3.up, teleportTargetPos.rotation * Vector3.forward);
    }
}
