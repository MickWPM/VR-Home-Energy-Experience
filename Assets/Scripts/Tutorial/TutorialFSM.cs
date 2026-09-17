using FSM;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class TutorialFSM : MonoBehaviour
{
    private FSM<NothingContext> stateMachine;
    private NothingContext nothingContext = new NothingContext();

    public TutorialStateSetupFromGO entryStateSetup, mainsBlackoutSetup;//, buttonInteractionSetup, teleportMenuSetup, rewiringSetup, consumerSelectedSetup, finalTestSetup;

    private void Start()
    {
        stateMachine = new FSM<NothingContext>();
        var entryState = new TutorialState(entryStateSetup.GetContext(), "Entry State");
        var mainsBlackout = new TutorialState(mainsBlackoutSetup.GetContext(), "Mains Blackout");


        stateMachine.AddTransition(entryState, mainsBlackout, (context) => MainsBreakTimeout());
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
