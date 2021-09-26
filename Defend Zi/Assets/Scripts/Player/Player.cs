using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    IFixedUpdate,
    IPositionAccessor,
    IPositionNotification,
    IHealthReincarnation,
    IScore
{
    private readonly IFixedUpdate _controlFixedUpdate;
    private readonly IHealthReincarnation _health = new PlayerHealth();
    private readonly IPosition _position;
    private readonly IScore _score = new PlayerScore();

    public Player(IUserInput input, Rigidbody2D rigidbody2D, PlayerMovementView movementView)
    {
        _position = new Rigidbody2DPosition(rigidbody2D);
        _controlFixedUpdate = new PlayerControl(input, _position, movementView);
    }

    Vector2 IPositionAccessor.Value => _position.Value;

    int IScoreAccessor.Value => _score.Value;

    int IHealthAccessor.Value => _health.Value;

    float IHealthAccessor.Percent => _health.Percent;

    event Action<int> IScoreNotification.OnReceived
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

    event Action IReincarnation.OnReviving
    {
        add => _health.OnReviving += value;
        remove => _health.OnReviving -= value;
    }

    event Action IPositionNotification.OnChanged
    {
        add => _position.OnChanged += value;
        remove => _position.OnChanged -= value;
    }

    void IFixedUpdate.Invoke(float deltaTime)
    {
        _controlFixedUpdate.Invoke(deltaTime);
    }

    void IDamageTaker.TakeDamage(IDamage damage) => _health.TakeDamage(damage);
    void IReincarnation.Revive() => _health.Revive();

    void IScoreCollector.Add(int amount) => _score.Add(amount);
}
