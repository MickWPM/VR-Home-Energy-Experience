using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerCircuit : MonoBehaviour
{
    public List<PowerConsumer> powerConsumers = new List<PowerConsumer>();
    public float MaxWattage
    {
        get => maxWattage;
        set => maxWattage = value;
    }
    public float LoadPercent => currentCircuitPower / maxWattage;
    [SerializeField] private float maxWattage = 30f;
    [SerializeField] private bool circuitOpen = true;
    public bool CircuitOpen => circuitOpen;
    [SerializeField] private bool powerAvailable = false;
    private float lastCircuitPower;
    public bool Energised
    {
        get => circuitOpen && powerAvailable;
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

    private void Start()
    {
        lastCircuitPower = -1f;
    }

    private float GetCurrentPower()
    {
        if (circuitOpen == false) return 0;
        if (powerAvailable == false) return 0;

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
        consumer.SetPowerAvailable(Energised);
    }

    public bool ContainsConsumer(PowerConsumer consumer)
    {
        return powerConsumers.Contains(consumer);
    }

    public System.Action<float> PowerDrawUpdatedEvent;
    private float currentCircuitPower;
    private void Update()
    {
        if (circuitOpen == false) return;

        currentCircuitPower = GetCurrentPower();
        if ( Mathf.Abs(lastCircuitPower - currentCircuitPower) > Mathf.Epsilon )
        {
            PowerDrawUpdatedEvent?.Invoke(currentCircuitPower);
        }
        lastCircuitPower = currentCircuitPower;
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
        if (circuitOpen) return;
        CircuitBreakerResetEvent?.Invoke();
        SetCircuitStatus(true);
    }

    public UnityEvent<bool> CircuitStatusUpdateEvent;
    public void SetCircuitStatus(bool enabled)
    {
        if (circuitOpen == enabled) return;
        circuitOpen = enabled;

        UpdateConsumerAvailability();
        CircuitStatusUpdateEvent?.Invoke(enabled);
    }

    public System.Action MainsPowerChangedToCircuitEvent;
    public void SetPowerSourceStatus(bool enabled)
    {
        if (powerAvailable == enabled) return;
        powerAvailable = enabled;
        UpdateConsumerAvailability();
        MainsPowerChangedToCircuitEvent?.Invoke();
    }

    private void UpdateConsumerAvailability()
    {
        foreach (var consumer in powerConsumers)
        {
            consumer.SetPowerAvailable(Energised);
        }
    }

    public string GetNiceSummary()
    {
        var currentDraw = circuitOpen && powerAvailable ? CurrentPowerOnLine : 0;
        var trippedString = circuitOpen ? string.Empty : " (TRIPPED)";
        return $"{gameObject.name}: {currentDraw}/ {MaxWattage}{trippedString}\\r\\n{powerConsumers.Count} connected consumers\\r\\n\\r\\n";
    }
}
