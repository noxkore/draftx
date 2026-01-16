using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycast
{
    bool Cast(out RaycastContext context);
}
