using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    MonoBehaviourExtContainer,
    IPositionAccessorNotifier,
    IPlayerHealth,
    IScore
{
    private readonly PlayerControl _playerControl;
    private readonly IPlayerHealth _health;
    private readonly IPositionAccessorNotifier _position;
    private readonly IScore _score;

    public Player(MonoBehaviourExt mono, IUserInput input, Rigidbody2D rigidbody2D, PlayerMovementData movementView) : base(mono)
    {
        IPosition position = new Rigidbody2DPosition(rigidbody2D);
        _playerControl = new PlayerControl(MonoBehaviourExt, input, position, movementView);
        _health = new PlayerHealth(mono, 1);
        _score = new PlayerScore();
        _position = position;
    }

    event Action<uint> IScoreNotification.OnReceived
    {
        add => _score.OnReceived += value;
        remove => _score.OnReceived -= value;
    }

    event Action IHealthNotification.WhenAlive
    {
        add => _health.WhenAlive += value;
        remove => _health.WhenAlive -= value;
    }

    event Action IHealthNotification.OnDamaged
    {
        add => _health.OnDamaged += value;
        remove => _health.OnDamaged -= value;
    }

    event Action IHealthNotification.OnDeath
    {
        add => _health.OnDeath += value;
        remove => _health.OnDeath -= value;
    }

    event Action IHealthNotification.WhenDead
    {
        add => _health.WhenDead += value;
        remove => _health.WhenDead -= value;
    }

    event Action IReincarnationNotification.OnReviving
    {
        add => _health.OnReviving += value;
        remove => _health.OnReviving -= value;
    }

    event Action IPositionNotifier.OnChanged
    {
        add => _position.OnChanged += value;
        remove => _position.OnChanged -= value;
    }

    public event Action WhenInvulnerable
    {
        add => _health.WhenInvulnerable += value;
        remove => _health.WhenInvulnerable -= value;
    }

    public event Action WhenVulnerable
    {
        add => _health.WhenVulnerable += value;
        remove => _health.WhenVulnerable -= value;
    }

    Vector2 IPositionAccessor.Value => _position.Value;

    uint IScoreAccessor.Value => _score.Value;

    int IHealthAccessor.Value => _health.Value;

    float IHealthAccessor.Percent => _health.Percent;


    void IDamageTaker.TakeDamage(IDamage damage) => _health.TakeDamage(damage);
    void IReincarnation.Revive() => _health.Revive();

    void IScoreCollector.Add(uint amount) => _score.Add(amount);
}
