using UnityEngine;

public class BreakerBoardOpeningTrigger : MonoBehaviour
{
    public BreakerBoardAnimations boardAnimations;
    private void OnTriggerEnter(Collider other)
    {
        boardAnimations.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        boardAnimations.CloseDoor();
    }
}
