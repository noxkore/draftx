using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrigger : RaycastBase, ITrigger
{
    public event Action<IContext> OnTriggered;

    public void TryTrigger()
    {
        if (Cast(out RaycastContext context))
        {
            OnTriggered?.Invoke(context);
        }
    }
}