using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    public event Action<IContext> OnTriggered;
}