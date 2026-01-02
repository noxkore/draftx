using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISufferModifier
{
    float Modify(float baseAmount, SufferContext context);
}
