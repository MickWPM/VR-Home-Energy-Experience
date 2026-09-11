using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject powerOutLight;
    public GameObject powerOnLight;

    private void Awake()
    {
        powerOutLight.SetActive(true);
        powerOnLight.SetActive(false);
    }

    [ContextMenu("Open Circuit")]
    public void OpenCircuit()
    {
        powerOutLight.SetActive(false);
        powerOnLight.SetActive(true);
    }
}
