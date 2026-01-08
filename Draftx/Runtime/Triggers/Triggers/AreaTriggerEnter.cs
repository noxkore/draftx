using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerEnter : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerContext triggerContext = new TriggerContext(collision.gameObject, collision);
        OnTriggered?.Invoke(triggerContext);
    }
}
