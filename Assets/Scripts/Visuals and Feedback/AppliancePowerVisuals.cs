using UnityEngine;

public class AppliancePowerVisuals : MonoBehaviour
{
    public MeshRenderer sourceRenderer;
    public PowerConsumer appliancePower;
    private Material applianceMat;
    private bool appliancePowerSwitchedOn = false;
    private static string POWERED_ON = "_PoweredOn";

    private void Awake()
    {
        if (appliancePower == null)
        {
            Debug.LogError("No power system associated with Appliance Power Visuals", gameObject);
            this.enabled = false;
        }
        applianceMat = sourceRenderer.material;
        UpdateShader();
    }


    [ContextMenu("Toggle Power")]
    public void TogglePower()
    {
        appliancePowerSwitchedOn = !appliancePowerSwitchedOn;
        UpdateShader();
    }

    public void SetPoweredOn()
    {
        appliancePowerSwitchedOn = true;
        UpdateShader();
    }

    public void SetPoweredOff()
    {
        appliancePowerSwitchedOn = false;
        UpdateShader();
    }

    private void UpdateShader()
    {
        applianceMat.SetFloat(POWERED_ON, appliancePowerSwitchedOn ? 1f : 0f);
    }

}
