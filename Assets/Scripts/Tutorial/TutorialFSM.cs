using FSM;
using UnityEngine;

public class TutorialFSM : MonoBehaviour
{
    private FSM<NothingContext> stateMachine;
    public  NothingContext nothingContext = new NothingContext();
    public string CurrentState => stateMachine.CurrentState;

    public TutorialStateSetupFromGO entryStateSetup;
    public TutorialMainsBreakStateSetupGO mainsBlackoutSetup;
    public TutorialStateSetupFromGO mainsRestoredSetup;
    public TutorialReiwringStateSetupGO rewiringToolActive;

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

        var selectedConsumer = new TutorialRewiringStates(rewiringToolActive.stateSetupMidRewire, "Rewiring consumer");
        stateMachine.AddState(selectedConsumer);
        stateMachine.AddTransition(rewiringSelected, selectedConsumer, rewiringToolActive.TransitionMetConsumerSelected);
        stateMachine.AddTransition(selectedConsumer, mainsRestored, rewiringToolActive.TransitionMetBackToToggle);
        //If we dont have the consumer selected it means we arent rewiring - this could be because we completed the rewire 
        //or we just dropped it off the node. This transition has a low priority and covers the "dropped off the node" case
        stateMachine.AddTransition(selectedConsumer, rewiringSelected, (context) => !rewiringToolActive.TransitionMetConsumerSelected(context), -10);
        //This transition catches the rewire success message and has the transition at a higher priority. 
        //Both events happen at the same frame so if we also get the success, the higher priority here will choose this transition
        //Thus the previous one is a fallback if this one doesnt happen.
        var finalState = new TutorialRewiringStates(rewiringToolActive.stateSetupPostRewire, "Exit State");
        stateMachine.AddState(finalState);
        stateMachine.AddTransition(selectedConsumer, finalState, rewiringToolActive.TransitionMetExitState, +10); 
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
