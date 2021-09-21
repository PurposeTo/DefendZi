using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class PlayerMono :
    MonoBehaviourExt,
    IPositionAccessor,
    IPositionNotification,
    IHealthReincarnation,
    IScore
{
    [SerializeField] private PlayerMovementData _movementData;

    private IFixedUpdate _fixedUpdate;
    private IHealthReincarnation _health;
    private IPositionAccessor _positionAccessor;
    private IPositionNotification _positionNotification;
    private IScore _score;

    [Inject]
    private void Constructor(IUserInput input, GameDifficulty gameDifficulty)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (gameDifficulty == null) throw new ArgumentNullException(nameof(gameDifficulty));

        Rigidbody2D rb2d = GetInitedComponent<Rigidbody2D>();
        PlayerMovementView movementView = new PlayerMovementView(gameDifficulty, _movementData);
        Player _player = new Player(input, rb2d, movementView);

        _fixedUpdate = _player;
        _positionAccessor = _player;
        _positionNotification = _player;
        _health = _player;
        _score = _player;
    }

    private void FixedUpdate() => _fixedUpdate.Invoke(Time.fixedDeltaTime);

    Vector2 IPositionAccessor.Value => _positionAccessor.Value;

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
        add => _positionNotification.OnChanged += value;
        remove => _positionNotification.OnChanged -= value;
    }

    void IReincarnation.Revive() => _health.Revive();
    void IDamageTaker.TakeDamage(uint damage) => _health.TakeDamage(damage);

    void IScoreCollector.Add(int amount) => _score.Add(amount);
}
