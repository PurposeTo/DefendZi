using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class PlayerMono :
    MonoBehaviourExt,
    IPositionAccessorNotifier,
    IHealthReincarnation,
    IScore
{
    [SerializeField] private PlayerMovementDataMono _movementData;

    private IHealthReincarnation _health;
    private IPositionAccessor _positionAccessor;
    private IPositionNotifier _positionNotification;
    private IScore _score;

    [Inject]
    private void Constructor(IUserInput input, GameDifficulty gameDifficulty)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (gameDifficulty == null) throw new ArgumentNullException(nameof(gameDifficulty));

        Rigidbody2D rb2d = GetInitedComponent<Rigidbody2D>();
        PlayerMovementData movementView = new PlayerMovementData(gameDifficulty, _movementData);
        Player _player = new Player(this, input, rb2d, movementView);

        _positionAccessor = _player;
        _positionNotification = _player;
        _health = _player;
        _score = _player;
    }

    Vector2 IPositionAccessor.Value => _positionAccessor.Value;

    uint IScoreAccessor.Value => _score.Value;

    int IHealthAccessor.Value => _health.Value;

    float IHealthAccessor.Percent => _health.Percent;

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
        add => _positionNotification.OnChanged += value;
        remove => _positionNotification.OnChanged -= value;
    }

    void IReincarnation.Revive() => _health.Revive();
    void IDamageTaker.TakeDamage(IDamage damage) => _health.TakeDamage(damage);

    void IScoreCollector.Add(uint amount) => _score.Add(amount);
}
