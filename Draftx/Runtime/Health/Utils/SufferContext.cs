using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SufferContext
{
    public DamageType Type;

    public static SufferContext Default => new SufferContext
    {
        Type = null
    };
}
