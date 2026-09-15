using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{
    public PowerSource mainsPower;
    public PowerCircuit circuit1, circuit2, circuit3;
    
    public TextMeshProUGUI mainsPowerText, circuit1Text, circuit2Text, circuit3Text;
    public Slider mainsPowerSlider, circuit1Slider, circuit2Slider, circuit3Slider;
    private Color defaultColour = Color.black, dirtyColour = Color.red;
    private bool[] isDirty = new bool[] { false, false, false, false};

    public GameObject revertButton;

    private void Awake()
    {
        Revert();
    }


    public void MainsSliderUpdated(float newValue)
    {
        bool dirty = (int)newValue != mainsPower.MaxWattage;
        isDirty[0] = dirty;
        Color col = dirty ? dirtyColour : defaultColour;
        UpdateSliderText(newValue, mainsPowerText, col);
    }

    public void Circuit1SsliderUpdate(float newValue)
    {
        CircuitSliderUpdated(circuit1, circuit1Text, newValue, 1);
    }
    public void Circuit2SsliderUpdate(float newValue)
    {
        CircuitSliderUpdated(circuit2, circuit2Text, newValue, 2);
    }
    public void Circuit3SsliderUpdate(float newValue)
    {
        CircuitSliderUpdated(circuit3, circuit3Text, newValue, 3);
    }

    public void CircuitSliderUpdated(PowerCircuit circuit, TextMeshProUGUI circuitText, float newValue, int circuitIndex)
    {
        bool dirty = (int)newValue != circuit.MaxWattage;
        isDirty[circuitIndex] = dirty;
        Color col = dirty ? dirtyColour : defaultColour;
        UpdateSliderText(newValue, circuitText, col);
    }


    [ContextMenu("Apply")]
    public void Apply()
    {
        if (isDirty[0])
        {
            isDirty[0] = false;
            mainsPower.MaxWattage = mainsPowerSlider.value;
            UpdateSliderText(mainsPower.MaxWattage, mainsPowerText, defaultColour);
        }


        if (isDirty[1])
        {
            isDirty[1] = false;
            circuit1.MaxWattage = circuit1Slider.value;
            UpdateSliderText(circuit1.MaxWattage, circuit1Text, defaultColour);
        }


        if (isDirty[2])
        {
            isDirty[2] = false;
            circuit2.MaxWattage = circuit2Slider.value;
            UpdateSliderText(circuit2.MaxWattage, circuit2Text, defaultColour);
        }


        if (isDirty[3])
        {
            isDirty[3] = false;
            circuit3.MaxWattage = circuit3Slider.value;
            UpdateSliderText(circuit3.MaxWattage, circuit3Text, defaultColour);
        }
    }

    [ContextMenu("Revert")]
    public void Revert()
    {
        mainsPowerSlider.value = mainsPower.MaxWattage;
        circuit1Slider.value = circuit1.MaxWattage;
        circuit2Slider.value = circuit2.MaxWattage;
        circuit3Slider.value = circuit3.MaxWattage;

        UpdateSliderText(mainsPower.MaxWattage, mainsPowerText, defaultColour);
        UpdateSliderText(circuit1.MaxWattage, circuit1Text, defaultColour);
        UpdateSliderText(circuit2.MaxWattage, circuit2Text, defaultColour);
        UpdateSliderText(circuit3.MaxWattage, circuit3Text, defaultColour);
    }

    private void CheckDirtyUpdate()
    {
        bool dirty = false;
        for (int i = 0; i < isDirty.Length; i++)
        {
            if (isDirty[i]) dirty = true;
        }

        revertButton.SetActive(dirty);
    }

    private void UpdateSliderText(float newValue, TextMeshProUGUI uiText, Color textColour)
    {
        uiText.color = textColour;
        uiText.text = newValue + "W";
        CheckDirtyUpdate();
    }
}
