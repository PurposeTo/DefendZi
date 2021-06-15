using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

public class PlayerMono :
    MonoBehaviourExt,
    IPositionGetter,
    IPositionNotification,
    IHealth,
    IScore
{
    [SerializeField, NotNull] private PlayerMovementData _movementData;

    private IFixedUpdate _fixedUpdate;
    private IHealth _health;
    private IPositionGetter _positionGetter;
    private IPositionNotification _positionNotification;
    private IScore _score;

    [Inject]
    private void Constructor(IUserInput input)
    {
        Player _player = new Player(input, GetComponent<Rigidbody2D>(), _movementData);

        _fixedUpdate = _player;
        _positionGetter = _player;
        _positionNotification = _player;
        _health = _player;
        _score = _player;
    }

    private void FixedUpdate() => _fixedUpdate.FixedUpdate(Time.fixedDeltaTime);

    Vector2 IPositionGetter.Value => _positionGetter.Value;

    IPercentable<int> IHealthGetter.Value => _health.Value;

    int IScoreGetter.Value => _score.Value;

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

    event Action IPositionNotification.OnChanged
    {
        add => _positionNotification.OnChanged += value;
        remove => _positionNotification.OnChanged -= value;
    }

    void IScoreCollector.Add(int amount) => _score.Add(amount);

    void IDamageTaker.TakeDamage(uint damage) => _health.TakeDamage(damage);
}
