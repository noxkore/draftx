using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastContext : IContext
{
    public readonly RaycastHit2D Hit;

    public GameObject Target => Hit.collider.gameObject;
    public Vector2 Point => Hit.point;
    public Vector2 Normal => Hit.normal;

    public RaycastContext(RaycastHit2D hit)
    {
        Hit = hit;
    }
}
