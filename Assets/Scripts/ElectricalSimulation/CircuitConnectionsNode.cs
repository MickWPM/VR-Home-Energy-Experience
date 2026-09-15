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

    public Transform circuit1Node, circuit2Node, circuit3Node;
    public Transform GetCircuitConnectionNode(int circuitID)
    {
        if (circuitID == 1) return circuit1Node;
        if (circuitID == 2) return circuit2Node;
        if (circuitID == 3) return circuit3Node;
        return transform;
    }
}
