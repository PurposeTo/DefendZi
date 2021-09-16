using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class PlayerMono :
    MonoBehaviourExt,
    IPositionGetter,
    IPositionNotification,
    IHealth,
    IScore
{
    [SerializeField] private PlayerMovementData _movementData;

    private IFixedUpdate _fixedUpdate;
    private IHealth _health;
    private IPositionGetter _positionGetter;
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
        _positionGetter = _player;
        _positionNotification = _player;
        _health = _player;
        _score = _player;
    }

    private void FixedUpdate() => _fixedUpdate.Invoke(Time.fixedDeltaTime);

    Vector2 IPositionGetter.Value => _positionGetter.Value;

    IPercentable<int> IHealthGetter.Value => _health.Value;

    int IScoreAccessor.Value => _score.Value;

    bool IDeath.IsDeath => _health.IsDeath;

    event Action IScoreNotification.OnReceived
    {
        add => _score.OnReceived += value;
        remove => _score.OnReceived -= value;
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
        add => _positionNotification.OnChanged += value;
        remove => _positionNotification.OnChanged -= value;
    }

    void IScoreCollector.Add(int amount) => _score.Add(amount);

    void IDamageTaker.TakeDamage(uint damage) => _health.TakeDamage(damage);
}
