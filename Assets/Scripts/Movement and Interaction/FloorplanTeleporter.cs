using System;
using UnityEngine;

public class FloorplanTeleporter : MonoBehaviour, ISerializationCallbackReceiver
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

    #region SceneVerification
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {        
        #if UNITY_EDITOR
        ValidateFields();
        #endif
    }

    private void ValidateFields()
    {
        if (XROrigin == null)
        {
            Debug.LogError("XR Origin not assigned to Floorplan Teleporter");
        }
        if (landryPos == null)
        {
            Debug.LogError("landryPos not assigned to Floorplan Teleporter");
        }
        if (kitchenPos == null)
        {
            Debug.LogError("kitchenPos not assigned to Floorplan Teleporter");
        }
        if (diningPos == null)
        {
            Debug.LogError("diningPos not assigned to Floorplan Teleporter");
        }
    }
    #endregion
}
