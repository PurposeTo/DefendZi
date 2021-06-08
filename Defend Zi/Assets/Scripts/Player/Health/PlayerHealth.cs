using System;
using Desdiene.Types.Percentable;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;

public class PlayerHealth : IHealth
{
    private IntPercentable health;

    public PlayerHealth()
    {
        int defaultHealth = 1;
        health = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
    }

    public event Action OnDied;
    IPercentable<int> IHealthGetter.Value => health;

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
