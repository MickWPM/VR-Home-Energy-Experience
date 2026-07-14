using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PowerSource : MonoBehaviour
{
    [SerializeField] private float MaxWattage = 30f;
    [SerializeField] private PowerCircuit[] attachedCircuits;
    [SerializeField] private bool SourceOpen = true;

    public float[] TMPDEBUG_currentCircuitConsumption;

    private void Awake()
    {
        if (attachedCircuits == null || attachedCircuits.Length == 0)
        {
            Debug.Log("Getting circuits", gameObject);
            List<PowerCircuit> circuits = new List<PowerCircuit>();
            for (int i = 0; i < transform.childCount; i++)
            {
                PowerCircuit powerCircuit = transform.GetChild(i).GetComponent<PowerCircuit>();
                if (powerCircuit != null )
                {
                    circuits.Add(powerCircuit);
                }
            }
            attachedCircuits = circuits.ToArray();
        }

        TMPDEBUG_currentCircuitConsumption = new float[attachedCircuits.Length];
    }

    private void Start()
    {
        foreach (var circuit in attachedCircuits)
        {
            circuit.SetPowerSourceStatus(SourceOpen);
        }
    }


    [SerializeField] float totalPowerDraw;
    private void Update()
    {
        if (SourceOpen == false)
        {
            SourceClosedUpdate();
        } else
        {
            SourceOpenUpdate();
        }
    }

    private void SourceClosedUpdate()
    {
        //Nothing!
    }

    private void SourceOpenUpdate()
    {
        var totalDesiredDraw = GetTotalDraw();

        if (totalDesiredDraw > MaxWattage)
        {
            TripBreaker();
        }
    }



    private float GetTotalDraw()
    {
        totalPowerDraw = 0;
        for (int i = 0; i < attachedCircuits.Length; i++)
        {
            var powerDraw = attachedCircuits[i].CurrentPowerOnLine;
            TMPDEBUG_currentCircuitConsumption[i] = powerDraw;
            totalPowerDraw += powerDraw;
        }

        return totalPowerDraw;
    }

    public UnityEvent<bool> PowerSourceBreakerStatusUpdateEvent;
    [ContextMenu("Reset Breaker")]
    public void ResetBreaker()
    {
        SetBreakerStatus(true);
    }


    public void TripBreaker()
    {
        SetBreakerStatus(false);
    }

    private void SetBreakerStatus(bool status)
    {
        if (status == SourceOpen) return;

        SourceOpen = status;
        PowerSourceBreakerStatusUpdateEvent?.Invoke(status);
        foreach (var circuit in attachedCircuits)
        {
            circuit.SetPowerSourceStatus(status);
        }

    }
}
