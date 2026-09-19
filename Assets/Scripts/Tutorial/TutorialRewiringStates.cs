using UnityEngine;
using FSM;

public class TutorialRewiringStates : FSM.State<NothingContext>
{
    public override string StateName => stateName;
    private string stateName;
    private TutorialReiwringStateSetup stateSetup;

    public TutorialRewiringStates(TutorialReiwringStateSetup setupData, string stateName)
    {
        this.stateName = stateName;
        this.stateSetup = setupData;
    }

    public override void EnterState(NothingContext context)
    {
        SetGoArrayStatus(stateSetup.enableOnEntry, true);
        SetGoArrayStatus(stateSetup.disableOnEntry, false);
        Debug.Log($"Entered {StateName}");
    }

    public override void ExitState(NothingContext context)
    {
        SetGoArrayStatus(stateSetup.enableOnEntry, false);
        Debug.Log($"Exited {StateName}");
    }

    private void SetGoArrayStatus(GameObject[] gameObjects, bool setEnabled)
    {
        if (gameObjects == null || gameObjects.Length == 0) return;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(setEnabled);
        }
    }
}
