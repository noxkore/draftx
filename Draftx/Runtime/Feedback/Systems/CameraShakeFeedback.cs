using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShakeFeedback : FeedbackBase
{
    [Header("Default Shake Settings")]
    [SerializeField] private float intensity = 1f;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private Vector2 direction = Vector2.one;

    [Header("Impulse Definition")]
    [SerializeField] private float maxAmplitude = 1f;
    [SerializeField] private float dissipationRate = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private AnimationCurve curve;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.m_DefaultVelocity.z = 0f;
    }

    public override void Play(IFeedbackContext context)
    {
        float finalIntensity = intensity;
        float finalDuration = duration;
        Vector2 finalDirection = direction;
        float finalAmplitude = maxAmplitude;
        float finalDissipation = dissipationRate;
        float finalFrequency = frequency;
        AnimationCurve finalCurve = curve;

        var def = impulseSource.m_ImpulseDefinition;
        def.m_ImpulseDuration = finalDuration;
        def.m_AmplitudeGain = finalAmplitude;
        def.m_DissipationRate = finalDissipation;
        def.m_FrequencyGain = finalFrequency;

        if (finalCurve != null)
        {
            def.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Custom;
            def.m_CustomImpulseShape = finalCurve;
        }

        Vector3 direction3D = new Vector3(
            finalDirection.x,
            finalDirection.y,
            0f
        ).normalized * finalIntensity;

        impulseSource.GenerateImpulse(direction3D);
    }
}