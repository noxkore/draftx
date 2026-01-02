using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthComponent
{
    float currentHealth { get; }
    float maxHealth { get; }

    event Action<float, float> OnHealthChanged;
    event Action<float> OnDamageTaken;
    event Action<float> OnHealed;
    event Action OnDeath;

    void Die();
    void SufferDamage(float ammount);
    void Heal(float ammount);
}
