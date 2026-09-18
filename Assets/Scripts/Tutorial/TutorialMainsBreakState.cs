using UnityEngine;
using FSM;

public class TutorialMainsBreakState : FSM.State<NothingContext>
{
    public override string StateName => stateName;
    private string stateName;
    TutorialMainsBreakStateSetup stateSetup;
    private float initialWattage;
    private float entryTime;
    public TutorialMainsBreakState(TutorialMainsBreakStateSetup setupData, string stateName)
    {
        this.stateName = stateName;
        this.stateSetup = setupData;
        initialWattage = stateSetup.powerSource.MaxWattage;

        if (stateSetup.objectsToEnable == null || stateSetup.objectsToEnable.Length == 0) gameobjectsEnabled = true;
    }

    public override void EnterState(NothingContext context)
    {
        stateSetup.powerSource.MaxWattage = stateSetup.capacityToSetOnEntry;
        this.entryTime = Time.timeSinceLevelLoad;
        if (stateSetup.objectsToDisable != null && stateSetup.objectsToDisable.Length > 0)
        {
            for (int i = 0; i < stateSetup.objectsToDisable.Length; i++)
            {
                stateSetup.objectsToDisable[i].SetActive(false);
            }
        }
        Debug.Log($"Entered {StateName}");
    }


    private bool circuitReset = false;
    private bool gameobjectsEnabled = false;

    public override void UpdateState(NothingContext context)
    {
        base.UpdateState(context);
        if (circuitReset == false)
        {
            stateSetup.powerSource.MaxWattage = initialWattage;
        }
        if (gameobjectsEnabled) return;

        float counter = Time.timeSinceLevelLoad - entryTime;
        if ( counter > stateSetup.enableObjectDelay)
        {
            for (int i = 0; i < stateSetup.objectsToEnable.Length; i++)
            {
                stateSetup.objectsToEnable[i].SetActive(true);
            }
            gameobjectsEnabled = true;
        }

    }

    public override void ExitState(NothingContext context)
    {
        Debug.Log($"Exited {StateName}");
    }

}
