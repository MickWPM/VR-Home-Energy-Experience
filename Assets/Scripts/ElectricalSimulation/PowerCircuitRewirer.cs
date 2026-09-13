using UnityEngine;

public class PowerCircuitRewirer : MonoBehaviour
{
    public PowerSource powerSource;

    public void RewireConsumer(PowerConsumer powerConsumer, int circuitID, bool turnOff = true)
    {
        if (turnOff) powerConsumer.SetPoweredOnStatus(false);
        for (int i = 0; i < powerSource.attachedCircuits.Length; i++)
        {
            if (powerSource.attachedCircuits[i].ContainsConsumer(powerConsumer))
                powerSource.attachedCircuits[i].RemoveConsumer(powerConsumer);
        }

        powerSource.attachedCircuits[circuitID].AddConsumer(powerConsumer);
    }
}
