using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircuitVisualiser : MonoBehaviour
{
    private static string EnergisedKey = "_Energised";
    private static string PowerDrawPercentKey = "_PowerDrawPercent";
    private Material material;
    [SerializeField] private PowerCircuit powerCircuit;
    private LineRenderer lineRenderer;
    
    private void Awake()
    {
        if (powerCircuit == null)
        {
            Debug.LogError("No power circuit provided to visualiser");
            this.enabled = false;
            return;
        }
        lineRenderer = GetComponent<LineRenderer>();
        material = lineRenderer.material;
    }

    private void Start()
    {
        SetupLineRenderer();
    }

    public Gradient noPowerGradient;
    public Gradient lowerPowerGradient;
    public Gradient highPowerGradient;
    private void SetupLineRenderer()
    {
        int numConsumers = powerCircuit.powerConsumers.Length;
        lineRenderer.positionCount = numConsumers + 1;
        Vector3[] positions = new Vector3[numConsumers + 1];
        positions[0] = powerCircuit.transform.position;
        for (int i = 0; i < numConsumers; i++) 
        {
            positions[i+1] = powerCircuit.powerConsumers[i].transform.position;
        }

        lineRenderer.SetPositions(positions);
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        var loadPercent = powerCircuit.CurrentPowerOnLine / powerCircuit.MaxWattage;
        //var lrColour = loadPercent > 0.75f ? highPowerGradient : lowerPowerGradient;
        material.SetFloat(PowerDrawPercentKey, loadPercent);

        float energised = powerCircuit.Energised ? 1f : 0f;
        material.SetFloat(EnergisedKey, energised);

        //if (powerCircuit.Energised == false) lrColour = noPowerGradient;
        //lineRenderer.colorGradient = lrColour;
    }


}
