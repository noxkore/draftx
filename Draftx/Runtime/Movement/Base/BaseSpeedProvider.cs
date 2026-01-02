using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpeedProvider : MonoBehaviour, ISpeedProvider
{
    [SerializeField] protected float baseSpeed;
    protected ISpeedModifier[] speedModifiers;
    protected void Awake()
    {
        UpdateModifiers();
    }
    public float GetSpeed()
    {
        float returnedSpeed = baseSpeed;
        for(int i = 0; i < speedModifiers.Length; i++)
        {
            returnedSpeed *= speedModifiers[i].GetMultiplier();
        }
        return returnedSpeed;
    }

    public void UpdateModifiers()
    {
        speedModifiers = GetComponents<ISpeedModifier>();
    }
}
