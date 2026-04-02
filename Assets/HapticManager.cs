using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class HapticManager : MonoBehaviour
{
    public HapticImpulsePlayer leftHaptic;
    public HapticImpulsePlayer rightHaptic;

    public void PlayGeneratorHaptic()
    {
        if (leftHaptic != null)
            leftHaptic.SendHapticImpulse(0.25f, 0.18f);

        if (rightHaptic != null)
            rightHaptic.SendHapticImpulse(0.25f, 0.18f);
    }

    public void PlayPowerupHaptic()
    {
        if (leftHaptic != null)
            leftHaptic.SendHapticImpulse(0.75f, 0.06f);

        if (rightHaptic != null)
            rightHaptic.SendHapticImpulse(0.75f, 0.06f);
    }

    public void PlayAppleHaptic()
    {
        if (leftHaptic != null)
            leftHaptic.SendHapticImpulse(0.55f, 0.20f);

        if (rightHaptic != null)
            rightHaptic.SendHapticImpulse(0.55f, 0.20f);
    }
}