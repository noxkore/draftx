using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTriggerInput : ToggleTriggerBase
{
    [SerializeField] private KeyCode key = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            TriggerContext context = new TriggerContext(gameObject, null);
            TryTrigger(context);
        }
    }

    public void ToggleTrigger()
    {
        Toggle();
    }

    public void EnableTrigger()
    {
        SetState(true);
    }

    public void DisableTrigger()
    {
        SetState(false);
    }
}
