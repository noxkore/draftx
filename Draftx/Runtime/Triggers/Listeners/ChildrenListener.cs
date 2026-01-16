using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenListener : ListenerBase
{
    protected override ITrigger[] ResolveTriggers()
    {
        List<ITrigger> triggers = new();

        foreach (Transform child in transform)
        {
            triggers.AddRange(child.GetComponents<ITrigger>());
        }

        return triggers.ToArray();
    }
}