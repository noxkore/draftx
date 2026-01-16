using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackAction : ActionBase
{
    private IFeedback[] feedbacks;

    protected void Start()
    {
        feedbacks = GetComponents<IFeedback>();
    }

    public override void Execute(IContext context)
    {
        if (feedbacks == null || feedbacks.Length == 0)
            return;

        if (context is not IFeedbackContext feedbackContext)
            return;

        foreach (var feedback in feedbacks)
        {
            feedback.Play(feedbackContext);
        }
    }
}