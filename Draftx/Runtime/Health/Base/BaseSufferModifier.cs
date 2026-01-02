using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSufferModifier : MonoBehaviour, ISufferModifier
{
    public abstract float Modify(float baseAmount, SufferContext context);
}
