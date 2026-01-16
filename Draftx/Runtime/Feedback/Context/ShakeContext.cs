using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeContext : IFeedbackContext, IContext
{
    public float Intensity;
    public float Duration;

    public Vector2 Direction;

    public float MaxAmplitude;
    public float DissipationRate;
    public float Frequency;

    public AnimationCurve Curve;

    public ShakeContext(
        float intensity,
        float duration,
        Vector2 direction,
        float maxAmplitude = 1f,
        float dissipationRate = 1f,
        float frequency = 1f,
        AnimationCurve curve = null)
    {
        Intensity = intensity;
        Duration = duration;
        Direction = direction == Vector2.zero ? Vector2.one : direction;

        MaxAmplitude = maxAmplitude;
        DissipationRate = dissipationRate;
        Frequency = frequency;

        Curve = curve;
    }
}