using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealthComponent : MonoBehaviour, IHealthComponent
{
    public float currentHealth { get; protected set; }
    public float maxHealth { get; protected set; }

    public event Action<float, float> OnHealthChanged;
    public event Action<float> OnDamageTaken;
    public event Action<float> OnHealed;
    public event Action OnDeath;

    public virtual void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public virtual void Heal(float ammount)
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnHealed?.Invoke(ammount);
        currentHealth += ammount;
    }

    public virtual void SufferDamage(float ammount)
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnDamageTaken?.Invoke(ammount);
        currentHealth -= ammount;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected void InvokeHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    protected void InvokeDamageTaken(float amount)
    {
        OnDamageTaken?.Invoke(amount);
    }

    protected void InvokeHealed(float amount)
    {
        OnHealed?.Invoke(amount);
    }

    protected void InvokeDeath()
    {
        OnDeath?.Invoke();
    }

}
