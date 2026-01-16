using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackAction : ActionBase
{
    private IFeedback[] feedbacks;

    protected override void Awake()
    {
        triggers = GetComponents<ITrigger>();
        foreach (ITrigger trigger in triggers)
        {
            trigger.OnTriggered += Execute;
        }

        feedbacks = GetComponents<IFeedback>();
    }

    public override void Execute(IContext context)
    {
        if (feedbacks == null || feedbacks.Length == 0)
            return;

        foreach (var feedback in feedbacks)
        {
            feedback.Play(new ShakeContext()) ;
        }
    }
}