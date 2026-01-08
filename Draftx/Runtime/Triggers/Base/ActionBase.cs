using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour, ITriggerAction
{
    protected ITrigger[] triggers;
    public virtual void Execute(IContext context)
    {
        
    }

    protected void Awake()
    {
        triggers = GetComponents<ITrigger>();
        foreach(ITrigger trigger in triggers)
        {
            trigger.OnTriggered += Execute;
        }
    }
    protected void OnDestroy()
    {
        if (triggers == null) return;

        foreach (ITrigger trigger in triggers)
        {
            trigger.OnTriggered -= Execute;
        }
    }

}
