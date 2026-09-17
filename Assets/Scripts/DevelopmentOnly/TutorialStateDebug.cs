using TMPro;
using UnityEngine;

public class TutorialStateDebug : MonoBehaviour
{
    public TutorialFSM fsm;
    public TextMeshProUGUI debugText;
    public TutorialReiwringStateSetupGO tutorialReiwringStateSetupGO;

    void Update()
    {
        debugText.text = $"Current state: {fsm.CurrentState}";
    }
}
