using System;
using Desdiene.Types.Percentable;
using Desdiene.Types.Percentale;
using Desdiene.Types.Range.Positive;

public class PlayerHealth : IHealth
{
    private IntPercentable _healthData;
    private event Action OnDied;
    private event Action OnReborn;
    private bool IsDeath;

    public PlayerHealth()
    {
        int defaultHealth = 1;
        _healthData = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
        OnDied += () => IsDeath = true;
        OnReborn += () => IsDeath = false;
    }

    event Action IDeath.OnDied
    {
        add => OnDied += value;
        remove => OnDied -= value;
    }

    event Action IDeath.OnReborn
    {
        add => OnReborn += value;
        remove => OnReborn -= value;
    }

    bool IDeath.IsDeath => IsDeath;
    IPercentable<int> IHealthGetter.Value => _healthData;

    void IDamageTaker.TakeDamage(uint damage)
    {
        _healthData -= damage;
        if (_healthData == 0) Die();
    }

    private void Die() => OnDied?.Invoke();
}
