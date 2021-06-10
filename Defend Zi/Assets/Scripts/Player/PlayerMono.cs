using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

public class PlayerMono :
    MonoBehaviourExt,
    IPositionGetter,
    IHealth,
    IScore
{
    [SerializeField] private PlayerMovementData _movementControlData;
    private Player _player;

    [Inject]
    private void Constructor(IUserInput input)
    {
        _player = new Player(input, GetComponent<Rigidbody2D>(), _movementControlData);
    }

    private void FixedUpdate() => _player.FixedUpdate(Time.fixedDeltaTime);

    Vector2 IPositionGetter.Value => ((IPositionGetter)_player).Value;

    IPercentable<int> IHealthGetter.Value => ((IHealthGetter)_player).Value;

    int IScoreGetter.Value => ((IScoreGetter)_player).Value;

    event Action IScoreNotification.OnScoreChanged
    {
        add => ((IScoreNotification)_player).OnScoreChanged += value;
        remove => ((IScoreNotification)_player).OnScoreChanged -= value;
    }

    event Action IDeath.OnDied
    {
        add => ((IDeath)_player).OnDied += value;
        remove => ((IDeath)_player).OnDied -= value;
    }

    void IScoreCollector.Add(int amount) => ((IScoreCollector)_player).Add(amount);

    void IDamageTaker.TakeDamage(uint damage) => ((IDamageTaker)_player).TakeDamage(damage);
}
