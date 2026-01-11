using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleTriggerBase : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    [SerializeField] protected bool startEnabled = true;

    protected bool isEnabled;

    protected virtual void Awake()
    {
        isEnabled = startEnabled;
    }

    protected void TryTrigger(IContext context)
    {
        if (!isEnabled)
            return;

        OnTriggered?.Invoke(context);
    }

    protected void Toggle()
    {
        isEnabled = !isEnabled;
    }

    protected void SetState(bool value)
    {
        isEnabled = value;
    }
}
