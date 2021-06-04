using System;
using Desdiene.Singleton.Unity;
using Desdiene.Types.Percentable;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : SceneSingleton<PlayerHealth>, IHealth<int>
{
    public event Action OnDied;
    private IntPercentable health;

    IPercentable<int> IHealth<int>.Health => health;

    protected override void AwakeSingleton()
    {
        int defaultHealth = 1;
        health = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
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
