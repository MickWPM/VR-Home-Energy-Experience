using System;
using UnityEngine;

public class CircuitConnectionPoint : MonoBehaviour
{
    public int circuitID;
    private PowerSource powerSource;
    public PowerSource PowerSource => powerSource;
    [SerializeField]private PowerCircuit circuit;
    public PowerCircuit Circuit => circuit;

    private void Awake()
    {
        powerSource = GameObject.FindAnyObjectByType<PowerSource>();
        circuit = powerSource.GetCircuitByID(circuitID);
    }
}
