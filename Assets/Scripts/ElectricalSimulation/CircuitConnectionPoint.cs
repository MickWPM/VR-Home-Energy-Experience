using System;
using UnityEngine;

public class CircuitConnectionPoint : MonoBehaviour
{
    public int circuitID;
    private PowerSource powerSource;
    [SerializeField]private PowerCircuit circuit;
    public PowerCircuit Circuit => circuit;

    private void Awake()
    {
        powerSource = GameObject.FindAnyObjectByType<PowerSource>();
        circuit = powerSource.GetCircuitByID(circuitID);
    }

    private void PowerDrawUpdated(float updatedPowerDraw)
    {
        float powerDrawProportion = updatedPowerDraw / circuit.MaxWattage;
    }

    private void OnEnable()
    {
        circuit.PowerDrawUpdatedEvent += PowerDrawUpdated;
    }
    private void OnDisable()
    {
        circuit.PowerDrawUpdatedEvent -= PowerDrawUpdated;
    }
}
