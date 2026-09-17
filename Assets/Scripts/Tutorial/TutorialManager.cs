using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public RewiringInteractionLogic rewiringInteractionLogic;

    public PowerConsumer endTutorialOnWiredConsumer;
    public PowerCircuit targetCircuit;
    public int targetCircuitID;
    public SceneControl sceneControl;

    private void Start()
    {
        rewiringInteractionLogic.ConsumerRewiredToCircuitEvent += RewiringInteractionLogic_ConsumerRewiredToCircuitEvent;
        rewiringInteractionLogic.ConsumerRewiredToCircuitIDEvent += RewiringInteractionLogic_ConsumerRewiredToCircuitIDEvent;
    }

    private void RewiringInteractionLogic_ConsumerRewiredToCircuitIDEvent(PowerConsumer consumer, int circuitID)
    {
        if (endTutorialOnWiredConsumer != consumer) return;
        if (targetCircuitID != circuitID) return;
        TutorialComplete();
    }

    private void RewiringInteractionLogic_ConsumerRewiredToCircuitEvent(PowerConsumer consumer, PowerCircuit circuit)
    {
        if (endTutorialOnWiredConsumer != consumer) return;
        if (targetCircuit != circuit) return;
        TutorialComplete();
    }

    public GameObject disableTest;
    private void TutorialComplete()
    {
        sceneControl.LoadMainScene();
    }
}
