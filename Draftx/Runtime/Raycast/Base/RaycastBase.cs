using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaycastBase : MonoBehaviour, IRaycast
{
    [Header("Raycast Settings")]
    [SerializeField] protected float Distance = 2f;
    [SerializeField] protected LayerMask Mask;
    [SerializeField] protected Vector2 Direction = Vector2.right;

    public virtual bool Cast(out RaycastContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.TransformDirection(Direction),
            Distance,
            Mask
        );

        if (hit.collider != null)
        {
            context = new RaycastContext(hit);
            return true;
        }

        context = null;
        return false;
    }
}
