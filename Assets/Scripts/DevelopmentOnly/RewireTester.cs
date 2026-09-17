using UnityEngine;

public class RewireTester : MonoBehaviour
{
    public PowerConsumer powerConsumer;
    public PowerCircuit circuit;
    public int circuitID;
    public RewiringInteractionLogic rewiringInteractionLogic;

    [ContextMenu("Rewire Test Circuit")]
    public void RewireTest()
    {
        rewiringInteractionLogic.ConsumerSelected(powerConsumer);
        rewiringInteractionLogic.PowerNodeHovered(circuit);
        rewiringInteractionLogic.ConsumerDeselected();
    }
    [ContextMenu("Rewire Test Circuit ID")]
    public void RewireTesIDt()
    {
        rewiringInteractionLogic.ConsumerSelected(powerConsumer);
        rewiringInteractionLogic.PowerNodeHovered(circuitID);
        rewiringInteractionLogic.ConsumerDeselected();
    }

}
