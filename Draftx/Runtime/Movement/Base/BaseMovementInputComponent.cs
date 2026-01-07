using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovementInputComponent
    : MonoBehaviour, IMovementInputComponent
{
    protected IMovementComponent movement;

    protected virtual void Awake()
    {
        movement = GetComponent<IMovementComponent>();
    }

    protected virtual void Update()
    {
        Tick();
    }

    public virtual void Tick()
    {
        if (movement == null)
            return;

        Vector2 direction = ReadDirection();
        movement.Move(direction);
    }
    protected abstract Vector2 ReadDirection();
}
