using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetector : RaycastBase
{
    public bool IsDetecting(out RaycastContext context)
    {
        return Cast(out context);
    }
}
