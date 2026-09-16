using UnityEngine;

public class OvenEmissionController : MonoBehaviour
{
    public Oven oven;
    public MeshRenderer ovenHeatBarMR;
    private Color baseColour;
    public float intensityMin = -1f, intensityMax = 10f;
    
    private void Awake()
    {
        baseColour = ovenHeatBarMR.material.color;
    }

    public void UpdateEmissionPercent(float percent)
    {
        float intensity = Mathf.Lerp(intensityMin, intensityMax, percent);
        //We can raise intensity to a power for more material slider like behevaiour
        //In tuning it is better to just leave as is; the colour selection will do what we need
        //intensity = Mathf.Pow(intensity, 3);    
        Color finalColor = baseColour * intensity;
        ovenHeatBarMR.material.SetColor("_EmissionColor", finalColor);
    }

    public void EnergisedUpdate(bool hasPower)
    {
        UpdateEmissionPercent(hasPower ? oven.CurrentTempPercent : intensityMin);
    }

    private void OnEnable()
    {
        oven.TemperaturePercentUpdated += UpdateEmissionPercent;
        UpdateEmissionPercent(oven.CurrentTempPercent);
    }

    private void OnDisable()
    {
        oven.TemperaturePercentUpdated -= UpdateEmissionPercent;
    }
}
