using UnityEngine;

public class AddRemoveConsumerTest : MonoBehaviour
{
    public PowerCircuit c1, c2, c3;
    public PowerConsumer washingmachine, kitchenlights;

    public PowerCircuitRewirer rewirer;

    void Start()
    {
        Debug.Log("Add remove consumer test");
        if (c1.ContainsConsumer(washingmachine)) Debug.Log("ContainsConsumer when contains works correctly");
        if (c2.ContainsConsumer(washingmachine) || c3.ContainsConsumer(washingmachine)) 
            Debug.LogError("Contains consumer returned true when it whouldn't have");

        rewirer.RewireConsumer(washingmachine, 1);
        if (c2.ContainsConsumer(washingmachine)) Debug.Log("Rewiring works correctly");
    }

}
