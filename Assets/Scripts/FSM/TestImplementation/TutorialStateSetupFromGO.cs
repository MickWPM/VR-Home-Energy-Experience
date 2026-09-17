using FSM;
using UnityEngine;

public class TutorialStateSetupFromGO : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;
    private TutorialStateSetup tutorialContext;
    private void Awake()
    {
        tutorialContext = new TutorialStateSetup();
        tutorialContext.objectsToEnable = objectsToEnable;
        tutorialContext.objectsToDisable = objectsToDisable;
    }

    public TutorialStateSetup GetContext()
    {
        return tutorialContext;
    }
}