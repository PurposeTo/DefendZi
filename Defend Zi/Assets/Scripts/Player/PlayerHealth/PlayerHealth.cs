using System;
using System.Collections.Generic;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percentale;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;

public partial class PlayerHealth : MonoBehaviourExtContainer, IPlayerHealth
{
    private readonly IStateSwitcher<State> _stateSwitcher;
    private readonly IRef<int> _health;
    private readonly IPercent _healthPercent;

    public PlayerHealth(MonoBehaviourExt mono, uint maxHealth) : base(mono)
    {
        int maxHealthInt = (int)maxHealth;
        IPercentable<int> health = new IntPercentable(maxHealthInt, new IntRange(0, maxHealthInt));
        _health = health;
        _healthPercent = health;

        State initState = new Alive(mono, this);
        List<State> allStates = new List<State>()
            {
                initState,
                new Dead(mono, this)
            };
        _stateSwitcher = new StateSwitcher<State>(initState, allStates);
    }

    private event Action WhenAlive;
    private event Action OnDamaged;
    private event Action OnDeath;
    private event Action WhenDead;
    private event Action OnReviving;
    private event Action WhenImmortal;
    private event Action WhenMortal;

    event Action IImmortalNotification.WhenImmortal
    {
        add
        {
            WhenImmortal = CurrentState.SubscribeToWhenImmortal(WhenImmortal, value);
        }
        remove => WhenImmortal -= value;
    }

    event Action IImmortalNotification.WhenMortal
    {
        add
        {
            WhenMortal = CurrentState.SubscribeToWhenMortal(WhenMortal, value);
        }
        remove => WhenMortal -= value;
    }

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

    private State CurrentState => _stateSwitcher.CurrentState;
}
