using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DMovementComponent : BaseMovementComponent
{
    [SerializeField] protected bool useVelocity = true;

    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Move(Vector2 direction)
    {
        if (speedProvider == null)
            return;

        if (direction.sqrMagnitude <= 0.0001f)
        {
            if (useVelocity)
                rb.velocity = Vector2.zero;

            return;
        }

        float speed = speedProvider.GetSpeed();
        Vector2 dir = direction.normalized;

        if (useVelocity)
        {
            rb.velocity = dir * speed;
        }
        else
        {
            rb.AddForce(dir * speed * Time.deltaTime, ForceMode2D.Force);

        }
    }
}
