using TMPro;
using UnityEngine;

public class BreakerBoardCircuitMonitor : MonoBehaviour
{
    [SerializeField]private PowerSource powerSource;
    [SerializeField]private TextMeshProUGUI mainsCircuitText;
    public GameObject mainsSwitch, circuit1Switch, circuit2Switch, circuit3Switch;
    //Mains breaker rotates on X, circuit rockers rotate on Y
    public float mainsOnRot, mainsOffRot, circuitRockerOnRot, circuitRockerOffRot;

    private void Start()
    {
        mainsCircuitText.text = $"Total Mains Power Rating: {powerSource.MaxWattage} W (Current draw {powerSource.TotalDesiredDraw})";
        SetBreakerStatus(powerSource.Energised);
        SetCircuit1Status(powerSource.attachedCircuits[0].Energised);
        SetCircuit2Status(powerSource.attachedCircuits[1].Energised);
        SetCircuit3Status(powerSource.attachedCircuits[2].Energised);
    }


    public void SetBreakerStatus(bool powerOn)
    {
        mainsSwitch.transform.localEulerAngles = new Vector3(powerOn ? mainsOnRot : mainsOffRot, 0, 0);
    }

    public void SetCircuit1Status(bool powerOn)
    {
        circuit1Switch.transform.localEulerAngles = new Vector3(0, powerOn ? circuitRockerOnRot : circuitRockerOffRot, 0);
    }
    public void SetCircuit2Status(bool powerOn)
    {
        circuit2Switch.transform.localEulerAngles = new Vector3(0, powerOn ? circuitRockerOnRot : circuitRockerOffRot, 0);
    }
    public void SetCircuit3Status(bool powerOn)
    {
        circuit3Switch.transform.localEulerAngles = new Vector3(0, powerOn ? circuitRockerOnRot : circuitRockerOffRot, 0);
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
