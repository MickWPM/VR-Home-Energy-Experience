using TMPro;
using UnityEngine;

public class RewiringInteractionLogic : MonoBehaviour
{
    private PowerConsumer selectedConsumer;
    private PowerCircuit hoveredCircuit;
    private int hoveredCircuitID = -1;
    public PowerCircuitRewirer rewirer;
    
    public void ConsumerSelected(PowerConsumer powerConsumer)
    {
        selectedConsumer = powerConsumer;
    }

    public event System.Action<PowerConsumer, PowerCircuit> ConsumerRewiredToCircuitEvent;
    public event System.Action<PowerConsumer, int> ConsumerRewiredToCircuitIDEvent;
    //If we are hovering over a circuit connection then we want to rewire
    public void ConsumerDeselected()
    {
        if (hoveredCircuit != null)
        {
            rewirer.RewireConsumer(selectedConsumer, hoveredCircuit);
            ConsumerRewiredToCircuitEvent?.Invoke(selectedConsumer, hoveredCircuit);
        }
        else if (hoveredCircuitID > -1)
        {
            rewirer.RewireConsumer(selectedConsumer, hoveredCircuitID);
            ConsumerRewiredToCircuitIDEvent?.Invoke(selectedConsumer, hoveredCircuitID);
        }
        selectedConsumer = null;
        hoveredCircuit = null;
        hoveredCircuitID = -1;
    }

    public void PowerNodeHovered(int circuitID)
    {
        hoveredCircuitID = circuitID;
    }

    public void PowerNodeHovered(PowerCircuit circuit)
    {
        hoveredCircuit = circuit;
    }
    public void PowerNodeUnhovered()
    {
        hoveredCircuit = null;
        hoveredCircuitID = -1;
    }
}
