using UnityEngine;

public class tmp_button_test : MonoBehaviour
{
    public MeshRenderer sourceRenderer;
    [SerializeField]private Material applianceMat;
    private bool energised = false;
    private static string POWERED_ON = "_PoweredOn";

    private void Awake()
    {
        applianceMat = sourceRenderer.material;
        UpdateShader();
    }

    [ContextMenu("Toggle Power")]
    public void TogglePower()
    {
        energised = !energised;
        UpdateShader();
    }

    public void PowerOn()
    {
        energised = true;
        UpdateShader();
    }

    public void PowerOff()
    {
        energised = false;
        UpdateShader();
    }

    private void UpdateShader()
    {
        applianceMat.SetFloat(POWERED_ON, energised ? 1f : 0f);
    }

}
