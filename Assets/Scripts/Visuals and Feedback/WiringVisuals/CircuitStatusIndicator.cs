using System;
using UnityEngine;

public class CircuitStatusIndicator : MonoBehaviour
{
    public CircuitConnectionPoint connectionPoint;
    private float loadMultiplier = 1f;
    public float pulseSpeed = 2f;
    public float scaleAmount = 0.2f;

    public MeshRenderer circuitStatusMeshRenderer;
    public Material greenMaterial, yellowMaterial, redMaterial, blackMaterial;
    public float greenThreshold = 0.4f, yellowThreshold = 0.66f;

    private Vector3 originalScale;
    private PowerCircuit circuit;

    void Awake()
    {
        originalScale = transform.localScale;
        CircuitPowerOff();
    }

    private void Start()
    {
        circuit = connectionPoint.Circuit;
        circuit.PowerDrawUpdatedEvent += SetCircuitLoad;

        circuit.CircuitStatusUpdateEvent.AddListener(CircuitStatusUpdated);
        circuit.MainsPowerAddedToCircuitEvent += MainsPowerAvailableForCircuit;
        if (circuit.Energised) MainsPowerAvailableForCircuit();
    }


    //https://docs.unity3d.com/6000.3/Documentation/Manual/execution-order.html
    //OnEnable runs between awake and start so the first event subscription needs to be done in Start
    //This is because we are using Start to get the circuit; the connection point gets it in awake
    //We could get the circuit in OnEnable but that is against the current project wide convention
    private void OnEnable()
    {
        if (circuit == null) return;
        circuit.PowerDrawUpdatedEvent += SetCircuitLoad;
        circuit.CircuitStatusUpdateEvent.AddListener(CircuitStatusUpdated);
    }
    private void OnDisable()
    {
        circuit.PowerDrawUpdatedEvent -= SetCircuitLoad;
        circuit.CircuitStatusUpdateEvent.RemoveListener(CircuitStatusUpdated);
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed * loadMultiplier) * scaleAmount;
        transform.localScale = originalScale + Vector3.one * pulse;
    }

    public void SetCircuitLoad(float totalLoad)
    {
        UpdateLoadPercent();
    }

    private void UpdateLoadPercent()
    {
        float loadPercent = circuit.LoadPercent;
        loadMultiplier = 1 + loadPercent;
        Material mat = redMaterial;
        if (loadPercent < yellowThreshold) mat = yellowMaterial;
        if (loadPercent < greenThreshold) mat = greenMaterial;

        circuitStatusMeshRenderer.material = mat;
    }

    private void CircuitStatusUpdated(bool powerOn)
    {
        if (!powerOn)
        {
            CircuitPowerOff();
            return;
        }
        UpdateLoadPercent();
    }

    private void CircuitPowerOff()
    {
        circuitStatusMeshRenderer.material = blackMaterial;
        loadMultiplier = 0.5f;
    }

    private void MainsPowerAvailableForCircuit()
    {
        UpdateLoadPercent();
    }
}
