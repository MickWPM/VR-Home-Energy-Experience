using FSM;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class TutorialFSM : MonoBehaviour
{
    private FSM<NothingContext> stateMachine;
    private NothingContext nothingContext = new NothingContext();

    public TutorialStateSetupFromGO entryStateSetup;
    public TutorialMainsBreakStateSetupGO mainsBlackoutSetup;
    public TutorialStateSetupFromGO mainsRestoredSetup;
    public TutorialReiwringStateSetupGO rewiringToolActive;
    //, rewiringSetup, consumerSelectedSetup, finalTestSetup;
    private void Start()
    {
        stateMachine = new FSM<NothingContext>();
        var entryState = new TutorialState(entryStateSetup.GetContext(), "Entry State");
        stateMachine.AddState(entryState);

        var mainsBlackout = new TutorialMainsBreakState(mainsBlackoutSetup.stateSetup, "Mains Blackout");
        stateMachine.AddState(mainsBlackout);
        stateMachine.AddTransition(entryState, mainsBlackout, (context) => MainsBreakTimeout());

        var mainsRestored = new TutorialState(mainsRestoredSetup.GetContext(), "Mains restored");
        stateMachine.AddState(mainsRestored);
        stateMachine.AddTransition(mainsBlackout, mainsRestored, mainsBlackoutSetup.TransitionMet);

        var rewiringSelected = new TutorialRewiringStates(rewiringToolActive.stateSetup, "Rewiring tool selected");
        stateMachine.AddState(rewiringSelected);
        stateMachine.AddTransition(mainsRestored, rewiringSelected, rewiringToolActive.TransitionMetObjectActive);
        stateMachine.AddTransition(rewiringSelected, mainsRestored, rewiringToolActive.TransitionMetBackToToggle);

        var selectedConsumer = new TutorialRewiringStates(rewiringToolActive.stateSetup, "Rewiring consumer");
        stateMachine.AddState(selectedConsumer);
        stateMachine.AddTransition(rewiringSelected, selectedConsumer, rewiringToolActive.TransitionMetConsumerSelected);
        stateMachine.AddTransition(selectedConsumer, mainsRestored, rewiringToolActive.TransitionMetBackToToggle);

        //If we are subscribed to the stop hover event this meets the transition but only if we didnt rewire
        //stateMachine.AddTransition(selectedConsumer, wiringToolActive, ...failed rewiring..., -10);
        //... add we have rewired....
        //stateMachine.AddTransition(selectedConsumer, we have rewired, ...rewire complete ..., +10);
    }

    private void Update()
    {
        stateMachine.Tick(nothingContext);
    }

    public float mainsBreakTimeout = 2f;
    public bool MainsBreakTimeout()
    {
        return Time.timeSinceLevelLoad > mainsBreakTimeout;
    }
}
