using System;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour, IDamageTaker
{
    public event Action OnDied;
    private IntInRange health;

    private void Awake()
    {
        int defaultHealth = 1;
        health = new IntInRange(defaultHealth, new IntRange(0, defaultHealth));
    }

    public void TakeDamage(uint damage)
    {
        health -= damage;
        if (health == 0) Die();
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
