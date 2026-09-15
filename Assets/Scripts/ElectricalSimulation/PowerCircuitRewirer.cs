using UnityEngine;

public class PowerCircuitRewirer : MonoBehaviour
{
    public PowerSource powerSource;

    public System.Action<PowerConsumer, PowerCircuit> ConsumerRewiredEvent;
    public void RewireConsumer(PowerConsumer powerConsumer, PowerCircuit circuit, bool turnOff = true)
    {
        if (turnOff) powerConsumer.SetPoweredOnStatus(false);


        for (int i = 0; i < powerSource.attachedCircuits.Length; i++)
        {
            if (powerSource.attachedCircuits[i].ContainsConsumer(powerConsumer))
                powerSource.attachedCircuits[i].RemoveConsumer(powerConsumer);
        }

        circuit.AddConsumer(powerConsumer);
        ConsumerRewiredEvent?.Invoke(powerConsumer, circuit);
    }

    public void RewireConsumer(PowerConsumer powerConsumer, int circuitID, bool turnOff = true)
    {
        RewireConsumer(powerConsumer, powerSource.attachedCircuits[circuitID], turnOff);
    }
}
