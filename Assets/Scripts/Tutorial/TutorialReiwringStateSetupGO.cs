using FSM;
using UnityEngine;

public class TutorialReiwringStateSetupGO : MonoBehaviour
{
    public TutorialReiwringStateSetup stateSetup;
    public TutorialReiwringStateSetup stateSetupMidRewire;
    public TutorialReiwringStateSetup stateSetupPostRewire;

    public bool ConsumerSelected => consumerSelected;
    private bool consumerSelected = false;
    public void TargetConsumerSelected(bool selected)
    {
        consumerSelected = selected;
    }

    public bool TransitionMetConsumerSelected(NothingContext nothingContext)
    {
        return consumerSelected;
    }

    public bool TransitionMetBackToToggle(NothingContext nothingContext)
    {
        return stateSetup.requiredActiveObject.activeInHierarchy == false;
    }

    public bool TransitionMetObjectActive(NothingContext nothingContext)
    {
        return stateSetup.requiredActiveObject.activeInHierarchy == true;
    }

    public bool TransitionMetExitState(NothingContext nothingContext)
    {
        return stateSetupPostRewire.requiredActiveObject.activeInHierarchy;
    }
}

[System.Serializable]
public class TutorialReiwringStateSetup
{
    public GameObject requiredActiveObject;

    public GameObject[] enableOnEntry;
    public GameObject[] disableOnEntry;
}