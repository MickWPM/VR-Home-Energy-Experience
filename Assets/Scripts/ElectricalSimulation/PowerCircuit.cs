using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerCircuit : MonoBehaviour
{
    public PowerConsumer[] powerConsumers;
    public float MaxWattage
    {
        get => maxWattage;
    }
    [SerializeField] private float maxWattage = 30f;
    [SerializeField] private bool CircuitOpen = true;
    [SerializeField] private bool PowerAvailable = false;
    public bool Energised
    {
        get => CircuitOpen && PowerAvailable;
    }

    public float CurrentPowerOnLine
    {
        get => GetCurrentPower(); 
    }


    private void Awake()
    {
        if (powerConsumers == null || powerConsumers.Length == 0)
        {
            Debug.Log("Getting consumers", gameObject);
            List<PowerConsumer> consumers = new List<PowerConsumer>();
            for (int i = 0; i < transform.childCount; i++)
            {
                PowerConsumer consumer = transform.GetChild(i).GetComponent<PowerConsumer>();
                if (consumer != null)
                {
                    consumers.Add(consumer);
                }
            }
            powerConsumers = consumers.ToArray();
        }
    }

    private float GetCurrentPower()
    {
        if (CircuitOpen == false) return 0;
        if (PowerAvailable == false) return 0;

        if (powerConsumers == null || powerConsumers.Length < 1) return 0;

        float power = 0;
        foreach (PowerConsumer consumer in powerConsumers)
        {
            power += consumer.CurrentPowerConsumption;
        }
        return power;
    }


    public float currentCircuitPower;
    private void Update()
    {
        if (CircuitOpen == false) return;

        currentCircuitPower = GetCurrentPower();
        if (currentCircuitPower > MaxWattage)
        {
            SetCircuitStatus(false);
            return;
        }
    }


    public UnityEvent CircuitBreakerResetEvent;
    [ContextMenu("Reset circuit breaker")]
    public void ResetCircuitBreaker()
    {
        if (CircuitOpen) return;
        CircuitBreakerResetEvent?.Invoke();
        SetCircuitStatus(true);
    }
    public void SetCircuitStatus(bool enabled)
    {
        if (CircuitOpen == enabled) return;
        CircuitOpen = enabled;
        bool powerAvailable = CircuitOpen ? PowerAvailable : false;
        foreach (var consumer in powerConsumers)
        {
            consumer.SetPowerAvailable(powerAvailable);
        }
    }

    public void SetPowerSourceStatus(bool enabled)
    {
        if (PowerAvailable == enabled) return;
        PowerAvailable = enabled;
        foreach (var consumer in powerConsumers)
        {
            consumer.SetPowerAvailable(enabled);
        }
    }

    public string GetNiceSummary()
    {
        var currentDraw = CircuitOpen && PowerAvailable ? CurrentPowerOnLine : 0;
        var trippedString = CircuitOpen ? string.Empty : " (TRIPPED)";
        return $"{gameObject.name}: {currentDraw}/ {MaxWattage}{trippedString}\\r\\n{powerConsumers.Length} connected consumers\\r\\n\\r\\n";
    }
}
