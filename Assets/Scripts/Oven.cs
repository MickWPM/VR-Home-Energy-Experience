
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] private PowerConsumer consumer;
    public float minTemp = 50, maxTemp = 300;
    [SerializeField] private float temperature;
    public float CurrentTemp => temperature;

    private void Awake()
    {
        this.temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
    }

    public void SetTemperaturePercent(float tempPct)
    {
        float temp = (maxTemp - minTemp) * tempPct + minTemp;
        this.temperature = Mathf.Clamp(temp, minTemp, maxTemp);
    }

    //Returns true if we are on, false if we are off
    public bool TogglePower()
    {
        consumer.TogglePoweredOnStatus();
        Debug.LogWarning("This wont work - we need events for power loss etc. Revisit ASAP");
        return consumer.Energised;
    }
}