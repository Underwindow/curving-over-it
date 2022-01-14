using System.Collections;
using UnityEngine.InputSystem.Haptics;
using UnityEngine;

public static class GamepadExtensions
{
    public static IEnumerator Vibrate(this IDualMotorRumble dualMotor, float lowFrequency, float highFrequency, float duration)
    {
        if (lowFrequency >= 1 || highFrequency >= 1)
            yield return null;
        
        dualMotor.SetMotorSpeeds(lowFrequency, highFrequency);

        yield return new WaitForSeconds(duration);

        dualMotor.ResetHaptics();
    }
}
