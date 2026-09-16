using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class HapticsFeedback : MonoBehaviour
{
    public HapticImpulsePlayer leftHaptics, rightHaptics;

    public HapticFeedbackDefinition mainsLostHaptics, mainsRestoredHaptics, circuitLostHaptics, circuitRestoredHaptics;

    public void MainsBreakerUpdate(bool poweredOn)
    {
        HapticFeedbackDefinition haptics = poweredOn ? mainsRestoredHaptics : mainsLostHaptics;
        bool successL = leftHaptics.SendHapticImpulse(haptics.amplitude, haptics.duration, haptics.frequency);
        bool successR = rightHaptics.SendHapticImpulse(haptics.amplitude, haptics.duration, haptics.frequency);
    }

    public void CircuitUpdate(bool poweredOn)
    {
        HapticFeedbackDefinition haptics = poweredOn ? circuitRestoredHaptics : circuitLostHaptics;
        //Right hand is the primary interaction so haptics on the left hand for circuit breaks
        //Initially right hand was the plan but it gets confused with the switch interaction haptics
        bool success = leftHaptics.SendHapticImpulse(haptics.amplitude, haptics.duration, haptics.frequency);
    }

    [System.Serializable]
    public class HapticFeedbackDefinition
    {
        [Range(0f, 1f)]
        public float amplitude = 0.5f;
        public float duration = 0.1f;
        public float frequency = 0f;

    }

}
