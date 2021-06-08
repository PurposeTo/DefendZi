using System;
using Desdiene.Types.Percentale;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    IPositionGetter,
    IHealth,
    IScoreCollector
{
    private readonly IUserInput userInput;
    private readonly PlayerControl control;
    private readonly IHealth health = new PlayerHealth();
    private readonly IPosition position;
    private readonly IScoreCollector scoreCollector = new PlayerScore();

    public Player(IUserInput input, Rigidbody2D rigidbody2D, PlayerMovementData movementControlData)
    {
        if (!rigidbody2D) throw new ArgumentNullException(nameof(rigidbody2D));
        if (!movementControlData)  throw new ArgumentNullException(nameof(movementControlData));

        userInput = input ?? throw new ArgumentNullException(nameof(input));
        position = new PlayerPosition(rigidbody2D);
        control = new PlayerControl(userInput, position, movementControlData);
    }

    Vector2 IPositionGetter.Value => position.Value;

    IPercentable<int> IHealthGetter.Value => health.Value;

    int IScore.Value => scoreCollector.Value;

    event Action IDeath.OnDied
    {
        add => health.OnDied += value;
        remove => health.OnDied -= value;
    }

    public void FixedUpdate(float deltaTime)
    {
        control.FixedUpdate(deltaTime);
    }

    void IDamageTaker.TakeDamage(uint damage) => health.TakeDamage(damage);

    void IScoreCollector.Add(int amount) => scoreCollector.Add(amount);
}
