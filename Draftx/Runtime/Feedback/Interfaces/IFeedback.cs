using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFeedback
{
    void Play(IFeedbackContext context);
}
