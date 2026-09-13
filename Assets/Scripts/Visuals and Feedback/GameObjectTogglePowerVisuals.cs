using UnityEngine;

public class GameObjectTogglePowerVisuals : MonoBehaviour
{
    public GameObject[] enabledOnlyWhenOn;
    public GameObject[] enabledOnlyWhenOff;

    public void SetPoweredOn()
    {
        UpdateBasedOnEnergisedStatus(true);
    }

    public void SetPoweredOff()
    {
        UpdateBasedOnEnergisedStatus(false);
    }

    public void UpdateBasedOnEnergisedStatus(bool poweredOn)
    {
        foreach (var go in enabledOnlyWhenOn)
        {
            go.SetActive(poweredOn);
        }
        foreach (var go in enabledOnlyWhenOff)
        {
            go.SetActive(!poweredOn);
        }
    }
}
