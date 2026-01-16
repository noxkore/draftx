using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IListener
{
    event Action<IContext> OnListened;
}
