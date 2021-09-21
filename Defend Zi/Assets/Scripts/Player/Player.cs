using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
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

    IPercentable<int> IHealthAccessor.Value => _health.Value;

    int IScoreAccessor.Value => _score.Value;

    bool IDeath.IsDeath => _health.IsDeath;

    event Action<int> IScoreNotification.OnReceived
    {
        add => _score.OnReceived += value;
        remove => _score.OnReceived -= value;
    }

    event Action IDeath.OnDied
    {
        add => _health.OnDied += value;
        remove => _health.OnDied -= value;
    }

    event Action IReincarnation.OnRevived
    {
        add => _health.OnRevived += value;
        remove => _health.OnRevived -= value;
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

    void IDamageTaker.TakeDamage(uint damage) => _health.TakeDamage(damage);
    void IReincarnation.Revive() => _health.Revive();

    void IScoreCollector.Add(int amount) => _score.Add(amount);
}
