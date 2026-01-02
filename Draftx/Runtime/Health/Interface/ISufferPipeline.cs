using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISufferPipeline
{
    void Suffer(float amount, SufferContext context);
}
