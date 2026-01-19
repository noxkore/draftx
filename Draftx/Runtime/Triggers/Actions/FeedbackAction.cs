using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackAction : ActionBase
{
    private IFeedback[] feedbacks;

    [Header("Runtime")]
    [SerializeField, Range(0f, 5f)]
    private float localIntensity = 1f;

    protected override void Awake()
    {
        triggers = GetComponents<ITrigger>();
        foreach (ITrigger trigger in triggers)
        {
            trigger.OnTriggered += Execute;
        }

        feedbacks = GetComponents<IFeedback>();
    }

    public void SetLocalIntensity(float value)
    {
        localIntensity = Mathf.Max(0f, value);
    }

    public override void Execute(IContext context)
    {
        if (feedbacks == null || feedbacks.Length == 0)
            return;

        for (int i = 0; i < feedbacks.Length; i++)
        {
            feedbacks[i].Play(null, localIntensity);
        }
    }
}