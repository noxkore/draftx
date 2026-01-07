using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovementComponent : MonoBehaviour, IMovementComponent
{
    protected ISpeedProvider speedProvider;
    protected virtual void Awake()
    {
        speedProvider = GetComponent<ISpeedProvider>();
    }

    public virtual void Move(Vector2 direction)
    {
        if (speedProvider == null)
            return;

        if (direction.sqrMagnitude <= 0.0001f)
            return;

        float speed = speedProvider.GetSpeed();
        Vector3 delta = (Vector3)direction.normalized * speed * Time.deltaTime;

        transform.position += delta;
    }
}