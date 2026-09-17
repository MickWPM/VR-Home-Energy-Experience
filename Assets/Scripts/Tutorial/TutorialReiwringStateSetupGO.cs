using FSM;
using UnityEngine;

public class TutorialReiwringStateSetupGO : MonoBehaviour
{
    public TutorialReiwringStateSetup stateSetup;


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
}

[System.Serializable]
public class TutorialReiwringStateSetup
{
    public GameObject requiredActiveObject;

    public GameObject[] enableOnEntry;
    public GameObject[] disableOnEntry;
}