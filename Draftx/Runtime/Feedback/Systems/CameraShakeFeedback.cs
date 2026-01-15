using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShakeFeedback : FeedbackBase
{
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.m_DefaultVelocity.z = 0f;
    }

    public override void Play(IFeedbackContext context)
    {
        if (context is not ShakeContext shake)
            return;

        var def = impulseSource.m_ImpulseDefinition;

        def.m_ImpulseDuration = shake.Duration;
        def.m_AmplitudeGain = shake.MaxAmplitude;
        def.m_DissipationRate = shake.DissipationRate;
        def.m_FrequencyGain = shake.Frequency;

        if (shake.Curve != null)
        {
            def.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Custom;
            def.m_CustomImpulseShape = shake.Curve;
        }

        Vector3 direction3D = new Vector3(
            shake.Direction.x,
            shake.Direction.y,
            0f
        ).normalized * shake.Intensity;

        impulseSource.GenerateImpulse(direction3D);
    }
}