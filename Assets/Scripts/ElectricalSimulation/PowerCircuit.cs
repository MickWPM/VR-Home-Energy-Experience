using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerCircuit : MonoBehaviour
{
    public List<PowerConsumer> powerConsumers = new List<PowerConsumer>();
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


    //Awake just lets us define a circuit by child elements.
    //This is not how it is currently set up in the main scene but it allows design flexibility
    private void Awake()
    {
        if (powerConsumers == null || powerConsumers.Count == 0)
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
            powerConsumers = consumers;
        }
    }

    private float GetCurrentPower()
    {
        if (CircuitOpen == false) return 0;
        if (PowerAvailable == false) return 0;

        if (powerConsumers == null || powerConsumers.Count < 1) return 0;

        float power = 0;
        foreach (PowerConsumer consumer in powerConsumers)
        {
            power += consumer.CurrentPowerConsumption;
        }
        return power;
    }

    public void RemoveConsumer(PowerConsumer consumer)
    {
        if (powerConsumers.Contains(consumer)) powerConsumers.Remove(consumer);
    }

    public void AddConsumer(PowerConsumer consumer)
    {
        powerConsumers.Add(consumer);
        consumer.SetPowerAvailable(PowerAvailable);
    }

    public bool ContainsConsumer(PowerConsumer consumer)
    {
        return powerConsumers.Contains(consumer);
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

    public UnityEvent<bool> CircuitStatusUpdateEvent;
    public void SetCircuitStatus(bool enabled)
    {
        if (CircuitOpen == enabled) return;
        CircuitOpen = enabled;
        CircuitStatusUpdateEvent?.Invoke(enabled);

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
        return $"{gameObject.name}: {currentDraw}/ {MaxWattage}{trippedString}\\r\\n{powerConsumers.Count} connected consumers\\r\\n\\r\\n";
    }
}
