using System;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player :
    MonoBehaviourExt,
    IPositionGetter,
    IHealth,
    IScoreCollector
{
    private IUserInput userInput;
    private PlayerControl control;
    private readonly IHealth health = new PlayerHealth();
    private IPosition position;
    private readonly IScoreCollector scoreCollector = new PlayerScore();

    [Inject]
    private void Constructor(IUserInput input)
    {
        userInput = input;
        position = new PlayerPosition(GetComponent<Rigidbody2D>());
        control = new PlayerControl(userInput, position, movementData);
    }

    [SerializeField] private PlayerMovementData movementData;

    Vector2 IPositionGetter.Value => position.Value;

    IPercentable<int> IHealthGetter.Value => health.Value;

    int IScore.Value => scoreCollector.Value;

    event Action IDeath.OnDied
    {
        add => health.OnDied += value;
        remove => health.OnDied -= value;
    }

    private void FixedUpdate()
    {
        control.FixedUpdate(Time.fixedDeltaTime);
    }

    void IDamageTaker.TakeDamage(uint damage) => health.TakeDamage(damage);

    void IScoreCollector.Add(int amount) => scoreCollector.Add(amount);
}
