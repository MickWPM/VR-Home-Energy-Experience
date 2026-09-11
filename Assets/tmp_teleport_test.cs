using UnityEngine;

public class tmp_teleport_test : MonoBehaviour
{
    public Transform XROrigin;
    public Transform landryPos, kitchenPos, diningPos;

    public void TeleportToLaundry()
    {
        TeleportToTransform(landryPos);
    }
    public void TeleportToKitchen()
    {
        TeleportToTransform(kitchenPos);
    }
    public void TeleportToDining()
    {
        TeleportToTransform(diningPos);
    }

    public void TeleportToTransform(Transform targetPos)
    {
        XROrigin.position = targetPos.position;
        XROrigin.rotation = targetPos.rotation;
    }
}
