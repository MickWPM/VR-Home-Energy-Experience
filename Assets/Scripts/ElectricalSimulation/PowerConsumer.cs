using UnityEngine;
using UnityEngine.Events;

public class PowerConsumer : MonoBehaviour
{
    [SerializeField]private bool poweredOn = false;
    [SerializeField]private float maxWattageConsumption = 5f;
    [SerializeField]private bool powerAvailable = false;
    [SerializeField] private bool currentEnergisedStatus;
    [SerializeField] private bool autoSwitchOffOnPowerFail = true;
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
        return poweredOn ? maxWattageConsumption : 0;
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
