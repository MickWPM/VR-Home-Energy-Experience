using TMPro;
using UnityEngine;

public class DevDebugCircuitUIManager : MonoBehaviour
{
    public TextMeshProUGUI summaryText;
    public PowerSource powerSource;


    private void Update()
    {
        summaryText.text = powerSource.GetNiceSummary();
    }
}
