using UnityEngine;

public class CircuitConnectionsNode : MonoBehaviour
{
    public RewiringInteractionLogic rewiringInteractionLogic;
    private PowerCircuitRewirer circuitRewirer;
    public PowerCircuitRewirer CircuitRewirer => circuitRewirer;

    private void Awake()
    {
        rewiringInteractionLogic = GameObject.FindAnyObjectByType<RewiringInteractionLogic>();
        circuitRewirer = GameObject.FindAnyObjectByType<PowerCircuitRewirer>();
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

    public CircuitConnectionPoint circuit1Node, circuit2Node, circuit3Node;
    public Transform GetCircuitConnectionNode(int circuitID)
    {
        if (circuitID == 1) return circuit1Node.transform;
        if (circuitID == 2) return circuit2Node.transform;
        if (circuitID == 3) return circuit3Node.transform;
        return transform;
    }
    public Transform GetCircuitConnectionNode(PowerCircuit circuit)
    {
        if (circuit == circuit1Node.Circuit) return circuit1Node.transform;
        if (circuit == circuit2Node.Circuit) return circuit2Node.transform;
        if (circuit == circuit3Node.Circuit) return circuit3Node.transform;
        
        return null;
    }
}
