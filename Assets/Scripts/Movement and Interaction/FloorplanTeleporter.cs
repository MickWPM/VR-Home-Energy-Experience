using System;
using UnityEngine;

public class FloorplanTeleporter : MonoBehaviour
{
    public Transform XROrigin;
    public Transform landryPos, kitchenPos, diningPos, breakerBox;

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
    public void TeleportToBreaker()
    {
        TeleportToTransform(breakerBox);
    }

    public void TeleportToTransform(Transform targetPos)
    {
        XROrigin.position = targetPos.position;
        XROrigin.rotation = targetPos.rotation;
    }

}
