using TMPro;
using UnityEngine;

public class TeleportDebug : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private FloorplanTeleporter teleporter;
    public Transform vrCamera, cameraOffset;
    private string teleportDebugString = "";

    private void Teleporter_BeforeTeleportEvent(Vector3 targetPos)
    {
        teleportDebugString = $"Teleporting to {targetPos}\nCam pos: {vrCamera.position}, Cam offset pos: {cameraOffset.position}";
    }

    private void Teleporter_AfterTeleportEvent(Vector3 targetPos)
    {
        teleportDebugString += $"\nAFTER TELEPORT:\nCam pos: {vrCamera.position}, Cam offset pos: {cameraOffset.position}";
        debugText.text = teleportDebugString;
    }

    private void OnEnable()
    {
        teleporter.BeforeTeleportEvent += Teleporter_BeforeTeleportEvent;
        teleporter.AfterTeleportEvent += Teleporter_AfterTeleportEvent;
    }


    private void OnDisable()
    {
        teleporter.BeforeTeleportEvent -= Teleporter_BeforeTeleportEvent;
        teleporter.AfterTeleportEvent -= Teleporter_AfterTeleportEvent;
    }
}
