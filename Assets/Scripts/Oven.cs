
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
        float pct = TempAsPowerPercent(this.temperature);
        consumer.SetPowerConsumptionPercent(pct);
    }

    public void SetTemperaturePercent(float tempPct)
    {
        float temp = TempFromPowerPercent(tempPct);
        this.temperature = Mathf.Clamp(temp, minTemp, maxTemp);
        consumer.SetPowerConsumptionPercent(tempPct);
    }

    public void TogglePower()
    {
        consumer.TogglePoweredOnStatus();
    }

    private float TempFromPowerPercent(float tempPct)
    {
        return (maxTemp - minTemp) * tempPct + minTemp;
    }
    private float TempAsPowerPercent(float temp)
    {
        float tempInRange = temp - minTemp;
        return tempInRange / (maxTemp - minTemp);
    }
}