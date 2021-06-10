using System;
using Desdiene.Types.Percentable;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;

public class PlayerHealth : IHealth
{
    private IntPercentable _healthData;

    public PlayerHealth()
    {
        int defaultHealth = 1;
        _healthData = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
    }

    public event Action OnDied;
    IPercentable<int> IHealthGetter.Value => _healthData;

    public void TakeDamage(uint damage)
    {
        _healthData -= damage;
        if (_healthData == 0) Die();
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
