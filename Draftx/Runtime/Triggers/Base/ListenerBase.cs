using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListenerBase : MonoBehaviour, IListener
{
    public event Action<IContext> OnListened;

    protected abstract ITrigger[] ResolveTriggers();

    protected virtual void Awake()
    {
        var triggers = ResolveTriggers();
        if (triggers == null) return;

        foreach (var trigger in triggers)
        {
            trigger.OnTriggered += HandleTriggered;
        }
    }

    protected virtual void OnDestroy()
    {
        var triggers = ResolveTriggers();
        if (triggers == null) return;

        foreach (var trigger in triggers)
        {
            trigger.OnTriggered -= HandleTriggered;
        }
    }

    protected void HandleTriggered(IContext context)
    {
        OnListened?.Invoke(context);
    }
}