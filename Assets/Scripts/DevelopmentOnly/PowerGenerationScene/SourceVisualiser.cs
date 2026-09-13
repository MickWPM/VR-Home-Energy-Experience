using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SourceVisualiser : MonoBehaviour
{

    private static string EnergisedKey = "_Energised";
    private static string PowerDrawPercentKey = "_PowerDrawPercent";
    private Material material;
    [SerializeField] private PowerSource powerSource;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        if (powerSource == null)
        {
            Debug.LogError("No power source provided to visualiser");
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
        int numCircuits = powerSource.attachedCircuits.Length;
        int numVerts = numCircuits * 2 + 1;
        lineRenderer.positionCount = numVerts;
        Vector3[] positions = new Vector3[numVerts];
        
        positions[0] = powerSource.transform.position;
        for (int i = 0; i < numCircuits; i++)
        {
            positions[i * 2 + 1] = powerSource.attachedCircuits[i].transform.position;
            positions[i * 2 + 2] = powerSource.transform.position;
        }


        lineRenderer.SetPositions(positions);
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        var loadPercent = powerSource.TotalDesiredDraw / powerSource.MaxWattage;
        material.SetFloat(PowerDrawPercentKey, loadPercent);

        float energised = powerSource.Energised ? 1f : 0f;
        material.SetFloat(EnergisedKey, energised);
        //var lrColour = loadPercent > 0.75f ? highPowerGradient : lowerPowerGradient;
        //if (powerSource.Energised == false) lrColour = noPowerGradient;
        //lineRenderer.colorGradient = lrColour;
    }
}
