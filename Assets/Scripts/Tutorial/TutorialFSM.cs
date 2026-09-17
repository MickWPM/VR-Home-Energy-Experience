using FSM;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class TutorialFSM : MonoBehaviour
{
    private FSM<NothingContext> stateMachine;
    private NothingContext nothingContext = new NothingContext();

    public TutorialStateSetupFromGO entryStateSetup;
    public TutorialMainsBreakStateSetupGO mainsBlackoutSetup;
    public TutorialStateSetupFromGO mainsRestoredSetup; //, rewiringSetup, consumerSelectedSetup, finalTestSetup;
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
