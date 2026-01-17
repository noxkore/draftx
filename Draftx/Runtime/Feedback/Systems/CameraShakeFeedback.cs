using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShakeFeedback : FeedbackBase
{
    [Header("Impulse Shake")]
    [SerializeField] private float intensity = 0.3f;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float dissipation = 0.8f;

    [Header("Continuous Shake")]
    [SerializeField] private float interval = 0.12f;
    [SerializeField] private float continuousDuration = 1.5f;

    private CinemachineImpulseSource impulse;

    private bool continuousActive;
    private float intervalTimer;
    private float durationTimer;

    private void Awake()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        impulse.m_DefaultVelocity = Vector3.zero;

        continuousActive = false;
    }

    private void Update()
    {
        if (!continuousActive)
            return;

        intervalTimer -= Time.deltaTime;
        durationTimer -= Time.deltaTime;

        if (intervalTimer <= 0f)
        {
            FireImpulse();
            intervalTimer = interval;
        }

        if (durationTimer <= 0f)
        {
            StopContinuous();
        }
    }

    public override void Play(IFeedbackContext context)
    {
        if (continuousDuration > 0f)
        {
            StartContinuous();
        }
        else
        {
            FireImpulse();
        }
    }

    private void FireImpulse()
    {
        var def = impulse.m_ImpulseDefinition;
        def.m_ImpulseDuration = duration;
        def.m_FrequencyGain = frequency;
        def.m_DissipationRate = dissipation;
        impulse.m_ImpulseDefinition = def;

        impulse.GenerateImpulse(Vector3.up * intensity);
    }

    private void StartContinuous()
    {
        continuousActive = true;
        intervalTimer = 0f;
        durationTimer = continuousDuration;
    }

    public void StopContinuous()
    {
        continuousActive = false;
    }
}
