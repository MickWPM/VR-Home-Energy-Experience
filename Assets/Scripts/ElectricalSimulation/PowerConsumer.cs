using UnityEngine;
using UnityEngine.Events;

public class PowerConsumer : MonoBehaviour
{
    [SerializeField]private bool poweredOn = false;
    [SerializeField]private float maxWattageConsumption = 5f, minWattageConsumption = 1f;
    [SerializeField]private bool powerAvailable = false;
    [SerializeField] private bool currentEnergisedStatus;
    [SerializeField] private bool autoSwitchOffOnPowerFail = true;
    private float powerConsumptionPercent = 1f;
    public bool AutoSwitchOffOnPowerFail => autoSwitchOffOnPowerFail;

    public bool Energised
    {
        get => poweredOn && powerAvailable; 
    }

    public float CurrentPowerConsumption
    {
        get => GetCurrentConsumption();
    }

    private float GetCurrentConsumption()
    {
        return poweredOn ? Mathf.Max(maxWattageConsumption * powerConsumptionPercent, minWattageConsumption) : 0;
    }

    public void Start()
    {
        if (Energised)
        {
            PoweredOnEvent?.Invoke();
        } else
        {
            PoweredOffEvent?.Invoke();
        }
    }

    public UnityEvent<bool> PowerAvailableUpdatedEvent;
    public void SetPowerAvailable(bool powerAvailable) 
    {
        if (this.powerAvailable != powerAvailable)
        {
            PowerAvailableUpdatedEvent?.Invoke(powerAvailable);
            this.powerAvailable = powerAvailable;
            CheckEnergisedStatusUpdate();
        }
    }

    [ContextMenu("Toggle power")]
    public void TogglePoweredOnStatus()
    {
        SetPoweredOnStatus(!poweredOn);
    }

    public UnityEvent PoweredOnEvent;
    public UnityEvent PoweredOffEvent;
    public void SetPoweredOnStatus(bool powerStatus)
    {
        if (powerStatus == poweredOn) return;

        //If we are trying to turn on but the circuit has no power, bail early but do this 
        if (powerStatus && this.powerAvailable == false)
        {
            //Only change our powered on state if we are a consumer that retains power state over power failure
            //eg. lights
            if (autoSwitchOffOnPowerFail == false)
            {
                poweredOn = powerStatus;
            }
            return;
        }
        poweredOn = powerStatus;

        CheckEnergisedStatusUpdate();

        if (poweredOn)
        {
            PoweredOnEvent?.Invoke();
        } else
        {
            PoweredOffEvent?.Invoke();
        }
    }

    public void SetPowerConsumptionPercent(float percent01)
    {
        if (percent01 < 0 || percent01 > 1)
        {
            Debug.LogWarning($"PowerConsumer::SetPowerConsumptionPercent should get percent between 0 and 1; got {percent01}");
        }
        percent01 = Mathf.Clamp01(percent01);
        powerConsumptionPercent = percent01;
    }

    public UnityEvent<bool> EnergisedStatusChangedEvent;
    private void CheckEnergisedStatusUpdate()
    {
        if (currentEnergisedStatus == Energised) return;
        currentEnergisedStatus = Energised;
        EnergisedStatusChangedEvent?.Invoke(Energised);

        if (autoSwitchOffOnPowerFail && currentEnergisedStatus == false)
        { 
            SetPoweredOnStatus(false);
        }
    }
}
