using FSM;
using UnityEngine;

public class  TutorialMainsBreakStateSetupGO : MonoBehaviour
{
    public TutorialMainsBreakStateSetup stateSetup;


    private bool mainsRestored = false;
    public void MainsPowerUpdated(bool powered)
    {
        mainsRestored = powered;
    }

    public bool TransitionMet(NothingContext nothingContext)
    {
        return mainsRestored;
    }
}

[System.Serializable]
public class TutorialMainsBreakStateSetup
{
    public PowerSource powerSource;
    public int capacityToSetOnEntry = 5;
    public GameObject[] objectsToEnable;
    public float enableObjectDelay = 3;
}