using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OvenPanel : MonoBehaviour
{
    public Oven oven;
    public TextMeshProUGUI temperatureText;
    public GameObject ovenTouchscreenPanel;
    public Color standbyColour = Color.black, poweredColour = Color.lightGreen;
    public Image powerBackgroundImage;
    

    private void Start()
    {
        UpdateTempText();
    }

    //Value is 0-0.5 to allow seamless image scaling. We update it here using the oven info
    public void SetTempProportion(float value)
    {
        //remap 0-1 by doubling
        float percent = value * 2;
        oven.SetTemperaturePercent(percent);
        UpdateTempText();
    }

    private void UpdateTempText()
    {
        temperatureText.text = oven.CurrentTemp + "\u00B0C";
    }

    [ContextMenu("TogglePowerButtonPressed")]
    public void TogglePowerButtonPressed()
    {
        oven.TogglePower();
    }

    public void OvenTurnedOn()
    {
        powerBackgroundImage.color = poweredColour;
    }

    public void OvenTurnedOff()
    {
        powerBackgroundImage.color = standbyColour;
    }


    [ContextMenu("Gain circuit power")]
    public void GainedPower()
    {
        ovenTouchscreenPanel.SetActive(true);
    }

    [ContextMenu("Lose circuit power")]
    public void LostPower()
    {
        ovenTouchscreenPanel.SetActive(false);
    }

    public void PowerAvailableUpdate(bool available)
    {
        if (available) GainedPower();
        else LostPower();
    }

}

