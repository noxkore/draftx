using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrigger : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    [SerializeField] private KeyCode key = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            TriggerContext context = new TriggerContext(gameObject, null);
            OnTriggered?.Invoke(context);
        }
    }
}
