using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    IFixedUpdate,
    IPositionGetter,
    IPositionNotification,
    IHealth,
    IScore
{
    private readonly IFixedUpdate _controlFixedUpdate;
    private readonly IHealth _health = new PlayerHealth();
    private readonly IPosition _position;
    private readonly IScore _score = new PlayerScore();

    public Player(IUserInput input, Rigidbody2D rigidbody2D, PlayerMovementView movementView)
    {
        _position = new Rigidbody2DPosition(rigidbody2D);
        _controlFixedUpdate = new PlayerControl(input, _position, movementView);
    }

    Vector2 IPositionGetter.Value => _position.Value;

    IPercentable<int> IHealthGetter.Value => _health.Value;

    int IScoreGetter.Value => _score.Value;

    bool IDeath.IsDeath => _health.IsDeath;

    event Action IScoreNotification.OnChanged
    {
        add => _score.OnChanged += value;
        remove => _score.OnChanged -= value;
    }

    event Action IDeath.OnDied
    {
        add => _health.OnDied += value;
        remove => _health.OnDied -= value;
    }

    event Action IDeath.OnReborn
    {
        add => _health.OnReborn += value;
        remove => _health.OnReborn -= value;
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

    void IScoreCollector.Add(int amount) => _score.Add(amount);
}
