using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percentale;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;

public partial class PlayerHealth : IHealthReincarnation
{
    private readonly IRef<State> _refCurrentState = new Ref<State>();

    private readonly IRef<int> _health;
    private readonly IPercent _healthPercent;

    public PlayerHealth()
    {
        int defaultHealth = 1;
        IPercentable<int> health = new IntPercentable(defaultHealth, new IntRange(0, defaultHealth));
        _health = health;
        _healthPercent = health;

        StateSwitcherWithContext<State, PlayerHealth> stateSwitcher = new StateSwitcherWithContext<State, PlayerHealth>(this, _refCurrentState);
        List<State> allStates = new List<State>()
            {
                new Alive(this, stateSwitcher),
                new Dead(this, stateSwitcher)
            };
        stateSwitcher.Add(allStates);
        stateSwitcher.Switch<Alive>();
    }

    private event Action WhenAlive;
    private event Action OnDamaged;
    private event Action OnDeath;
    private event Action WhenDead;
    private event Action OnReviving;

    event Action IHealthNotification.WhenAlive
    {
        add
        {
            WhenAlive = CurrentState.SubscribeToWhenAlive(WhenAlive, value);
        }
        remove => WhenAlive -= value;
    }

    event Action IHealthNotification.OnDamaged
    {
        add => OnDamaged += value;
        remove => OnDamaged -= value;
    }

    event Action IHealthNotification.OnDeath
    {
        add => OnDeath += value;
        remove => OnDeath -= value;
    }

    event Action IHealthNotification.WhenDead
    {
        add
        {
            WhenDead = CurrentState.SubscribeToWhenDead(WhenDead, value);
        }
        remove => WhenDead -= value;
    }

    event Action IReincarnationNotification.OnReviving
    {
        add => OnReviving += value;
        remove => OnReviving -= value;
    }

    int IHealthAccessor.Value => _health.Value;

    float IHealthAccessor.Percent => _healthPercent.Value;

    void IDamageTaker.TakeDamage(IDamage damage) => CurrentState.TakeDamage(damage);

    void IReincarnation.Revive() => CurrentState.Revive();

    private State CurrentState => _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));
}
