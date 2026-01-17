using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BaseSufferPipeline : MonoBehaviour, ISufferPipeline
{
    protected BaseHealthComponent health;
    protected ISufferModifier[] modifiers;

    protected virtual void Awake()
    {
        health = GetComponent<BaseHealthComponent>();
        modifiers = GetComponents<ISufferModifier>();
    }

    public virtual void Suffer(float amount, SufferContext context)
    {
        if (context.Equals(default))
            context = SufferContext.Default;

        float finalAmount = ProcessModifiers(amount, context);
        ApplyDamage(finalAmount, context);
    }

    protected virtual float ProcessModifiers(float amount, SufferContext context)
    {
        float finalAmount = amount;

        for (int i = 0; i < modifiers.Length; i++)
            finalAmount = modifiers[i].Modify(finalAmount, context);

        return finalAmount;
    }

    protected virtual void ApplyDamage(float amount, SufferContext context)
    {
        health.SufferDamage(amount);
    }
}