using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    public event Action<GameObject> OnTriggered;
}