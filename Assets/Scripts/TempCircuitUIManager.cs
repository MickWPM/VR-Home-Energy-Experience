using TMPro;
using UnityEngine;

public class TempCircuitUIManager : MonoBehaviour
{
    public TextMeshProUGUI summaryText;
    public PowerSource powerSource;


    private void Update()
    {
        summaryText.text = powerSource.GetNiceSummary();
    }
}
