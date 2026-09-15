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
        intensity = Mathf.Pow(intensity, 3);    //This mimics the unity material slider
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
