using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerContext : IContext
{
    public GameObject Source { get; }
    public Collider2D Collider { get; }

    public TriggerContext(GameObject source, Collider2D collider)
    {
        Source = source;
        Collider = collider;
    }
}
