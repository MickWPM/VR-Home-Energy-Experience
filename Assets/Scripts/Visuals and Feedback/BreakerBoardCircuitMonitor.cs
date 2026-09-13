using TMPro;
using UnityEngine;

public class BreakerBoardCircuitMonitor : MonoBehaviour
{
    [SerializeField]private PowerSource powerSource;
    [SerializeField] private TextMeshProUGUI mainsCircuitText, circuit1Text, circuit2Text, circuit3Text;

    private void Start()
    {
        mainsCircuitText.text = $"Total Mains Power Rating: {powerSource.MaxWattage} W (Current draw {powerSource.TotalDesiredDraw})";
        circuit1Text.text = ""; //$"Circuit 1 Rating: {powerSource.attachedCircuits[0].MaxWattage} W (Current draw {powerSource.TMPDEBUG_currentCircuitConsumption[0]})";
        circuit2Text.text = ""; //$"Circuit 2 Rating: {powerSource.attachedCircuits[1].MaxWattage} W (Current draw {powerSource.TMPDEBUG_currentCircuitConsumption[1]})";
        circuit3Text.text = ""; //$"Circuit 3 Rating: {powerSource.attachedCircuits[2].MaxWattage} W (Current draw {powerSource.TMPDEBUG_currentCircuitConsumption[2]})";
    }


    public GameObject mainsSwitch, circuit1Switch, circuit2Switch, circuit3Switch;
    public void SetBreakerStatus(bool powerOn)
    {
        mainsSwitch.SetActive(powerOn);
    }

    public void SetCircuit1Status(bool powerOn)
    {
        circuit1Switch.SetActive(powerOn);
    }
    public void SetCircuit2Status(bool powerOn)
    {
        circuit2Switch.SetActive(powerOn);
    }
    public void SetCircuit3Status(bool powerOn)
    {
        circuit3Switch.SetActive(powerOn);
    }

    private void Update()
    {
        mainsCircuitText.text = powerSource.GetNiceSummary();
    }


    private void OnEnable()
    {
        powerSource.PowerSourceBreakerStatusUpdateEvent.AddListener(SetBreakerStatus);
        powerSource.attachedCircuits[0].CircuitStatusUpdateEvent.AddListener(SetCircuit1Status);
        powerSource.attachedCircuits[1].CircuitStatusUpdateEvent.AddListener(SetCircuit2Status);
        powerSource.attachedCircuits[2].CircuitStatusUpdateEvent.AddListener(SetCircuit3Status);
    }
    private void OnDisable()
    {
        powerSource.PowerSourceBreakerStatusUpdateEvent.RemoveListener(SetBreakerStatus);
        powerSource.attachedCircuits[0].CircuitStatusUpdateEvent.RemoveListener(SetCircuit1Status);
        powerSource.attachedCircuits[1].CircuitStatusUpdateEvent.RemoveListener(SetCircuit2Status);
        powerSource.attachedCircuits[2].CircuitStatusUpdateEvent.RemoveListener(SetCircuit3Status);
    }
}
