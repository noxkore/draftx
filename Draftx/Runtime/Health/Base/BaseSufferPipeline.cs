using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BaseSufferPipeline : MonoBehaviour, ISufferPipeline
{
    private BaseHealthComponent health;
    private ISufferModifier[] modifiers;

    private void Awake()
    {
        health = GetComponent<BaseHealthComponent>();
        modifiers = GetComponents<ISufferModifier>();
    }

    public void Suffer(float amount, SufferContext context)
    {
        float finalAmount = amount;

        for (int i = 0; i < modifiers.Length; i++)
            finalAmount = modifiers[i].Modify(finalAmount, context);

        health.SufferDamage(finalAmount);
    }
}