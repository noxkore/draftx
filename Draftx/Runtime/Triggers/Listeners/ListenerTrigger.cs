using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerTrigger : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    private IListener[] listeners;

    protected void Awake()
    {
        listeners = GetComponents<IListener>();

        foreach (var listener in listeners)
        {
            listener.OnListened += HandleListened;
        }
    }

    protected void OnDestroy()
    {
        if (listeners == null) return;

        foreach (var listener in listeners)
        {
            listener.OnListened -= HandleListened;
        }
    }

    private void HandleListened(IContext context)
    {
        OnTriggered?.Invoke(context);
    }
}