using System;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percentale;
using Desdiene.Types.Ranges.Positive;

public class PlayerHealth : IHealthReincarnation
{
    private IntPercentable _healthData;
    private event Action OnDied;
    private event Action OnRevived;
    private bool IsDeath;

    public PlayerHealth()
    {
        int defaultHealth = 1;
        _healthData = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
        OnDied += () => IsDeath = true;
        OnRevived += () => IsDeath = false;
    }

    event Action IDeath.OnDied
    {
        add => OnDied += value;
        remove => OnDied -= value;
    }

    event Action IReincarnation.OnRevived
    {
        add => OnRevived += value;
        remove => OnRevived -= value;
    }

    bool IDeath.IsDeath => IsDeath;
    IPercentable<int> IHealthAccessor.Value => _healthData;

    void IDamageTaker.TakeDamage(uint damage)
    {
        _healthData -= damage;
        if (_healthData.IsMin) Die();
    }

    void IReincarnation.Revive()
    {
        throw new NotImplementedException();
    }

    private void Die() => OnDied?.Invoke();
}
