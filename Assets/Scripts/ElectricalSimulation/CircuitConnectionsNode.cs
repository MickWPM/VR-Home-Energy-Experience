using UnityEngine;

public class CircuitConnectionsNode : MonoBehaviour
{
    public RewiringInteractionLogic rewiringInteractionLogic;
    private void Awake()
    {
        rewiringInteractionLogic = GameObject.FindAnyObjectByType<RewiringInteractionLogic>();
    }

    public void SetHoveredCircuit(PowerCircuit circuit)
    {
        rewiringInteractionLogic.PowerNodeHovered(circuit);
    }

    public void SetHoveredCircuit(int circuitID)
    {
        rewiringInteractionLogic.PowerNodeHovered(circuitID);
    }

    public void ClearHoveredCircuit()
    {
        rewiringInteractionLogic.PowerNodeUnhovered();
    }
}
