using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildListener : ListenerBase
{
    protected override ITrigger[] ResolveTriggers()
    {
        return GetComponentsInChildren<ITrigger>();
    }
}
