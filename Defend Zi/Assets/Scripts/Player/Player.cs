using System;
using Desdiene.Types.Percentale;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    IPositionGetter,
    IHealth,
    IScore
{
    private readonly IUserInput _userInput;
    private readonly PlayerControl _control;
    private readonly IHealth _health = new PlayerHealth();
    private readonly IPosition _position;
    private readonly IScore _score = new PlayerScore();

    public Player(IUserInput input, Rigidbody2D rigidbody2D, PlayerMovementData movementControlData)
    {
        if (!rigidbody2D) throw new ArgumentNullException(nameof(rigidbody2D));
        if (!movementControlData) throw new ArgumentNullException(nameof(movementControlData));

        _userInput = input ?? throw new ArgumentNullException(nameof(input));
        _position = new PlayerPosition(rigidbody2D);
        _control = new PlayerControl(_userInput, _position, movementControlData);
    }

    Vector2 IPositionGetter.Value => _position.Value;

    IPercentable<int> IHealthGetter.Value => _health.Value;

    int IScoreGetter.Value => _score.Value;

    public event Action OnScoreChanged
    {
        add => _score.OnScoreChanged += value;
        remove => _score.OnScoreChanged -= value;
    }

    event Action IDeath.OnDied
    {
        add => _health.OnDied += value;
        remove => _health.OnDied -= value;
    }

    public void FixedUpdate(float deltaTime)
    {
        _control.FixedUpdate(deltaTime);
    }

    void IDamageTaker.TakeDamage(uint damage) => _health.TakeDamage(damage);

    void IScoreCollector.Add(int amount) => _score.Add(amount);
}
