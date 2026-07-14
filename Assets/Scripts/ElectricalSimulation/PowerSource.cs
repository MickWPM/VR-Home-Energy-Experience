using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PowerSource : MonoBehaviour
{
    public float MaxWattage
    {
        get => maxWattage;
    }
    [SerializeField] private float maxWattage = 30f;
    public PowerCircuit[] attachedCircuits;
    public bool Energised { get => SourceOpen; }
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

    private float totalDesiredDraw;
    public float TotalDesiredDraw { get => totalDesiredDraw; }
    private void SourceOpenUpdate()
    {
        totalDesiredDraw = GetTotalDraw();

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

    public string GetNiceSummary()
    {
        float currentDraw = SourceOpen ? GetTotalDraw() : 0;
        string trippedMessage = SourceOpen ? string.Empty : " (TRIPPED)";
        string s = $"Power Network Summary\r\nSource: {currentDraw} / {MaxWattage}{trippedMessage}\r\n{attachedCircuits.Length} connected circuits\r\n\r\n";
        //add each circuit message
        foreach (var circuit in attachedCircuits)
        {
            s+= circuit.GetNiceSummary();
        }

        return s;
    }
}
