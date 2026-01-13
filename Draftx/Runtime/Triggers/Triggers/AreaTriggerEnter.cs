using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTriggerEnter : MonoBehaviour, ITrigger
{
    [SerializeField] private string requiredTag = "Player";
    public event Action<IContext> OnTriggered;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(requiredTag)) return;
        TriggerContext triggerContext = new TriggerContext(collision.gameObject, collision);
        OnTriggered?.Invoke(triggerContext);
    }
}
