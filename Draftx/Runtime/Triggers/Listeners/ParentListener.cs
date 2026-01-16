using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParentListener : ListenerBase
{
    protected override ITrigger[] ResolveTriggers()
    {
        if (transform.parent == null)
            return null;

        return transform.parent.GetComponents<ITrigger>();
    }
}
