using UnityEngine;

public class TutorialCompleteFlagger : MonoBehaviour
{
    public GameObject previosulyCompletedUI;
    private const string TUTORIAL_COMPLETE_KEY = "TutorialComplete";

    private void Awake()
    {
        int completeFlag = PlayerPrefs.GetInt(TUTORIAL_COMPLETE_KEY, 0);
        previosulyCompletedUI.SetActive(completeFlag > 0);
    }

    public void TutorialCompleted()
    {
        PlayerPrefs.SetInt(TUTORIAL_COMPLETE_KEY, 1);
    }

    public void ClearCompleteFlag()
    {
        PlayerPrefs.SetInt(TUTORIAL_COMPLETE_KEY, 0);
    }
}
