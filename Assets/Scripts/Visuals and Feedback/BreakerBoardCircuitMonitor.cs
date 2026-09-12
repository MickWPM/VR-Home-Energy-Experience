using TMPro;
using UnityEngine;

public class BreakerBoardCircuitMonitor : MonoBehaviour
{
    [SerializeField]private PowerSource powerSource;
    [SerializeField] private TextMeshProUGUI mainsCircuitText, circuit1Text, circuit2Text, circuit3Text;

    private void Start()
    {
        mainsCircuitText.text = $"Total Mains Power Rating: {powerSource.MaxWattage} W";
        circuit1Text.text = $"Circuit 1 Rating: {powerSource.attachedCircuits[0].MaxWattage} W";
        circuit2Text.text = $"Circuit 2 Rating: {powerSource.attachedCircuits[1].MaxWattage} W";
        circuit3Text.text = $"Circuit 3 Rating: {powerSource.attachedCircuits[2].MaxWattage} W";
    }
}
