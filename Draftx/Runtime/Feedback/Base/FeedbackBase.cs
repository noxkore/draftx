using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedbackBase : MonoBehaviour, IFeedback
{
    public abstract void Play(IFeedbackContext context, float localIntensity);
}
