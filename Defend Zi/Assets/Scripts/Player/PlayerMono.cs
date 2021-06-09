using System;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Types.Percentale;
using UnityEngine;
using Zenject;

public class PlayerMono :
    MonoBehaviourExt,
    IPositionGetter,
    IHealth,
    IScore
{
    [SerializeField] private PlayerMovementData movementControlData;
    private Player player;

    [Inject]
    private void Constructor(IUserInput input)
    {
        player = new Player(input, GetComponent<Rigidbody2D>(), movementControlData);
    }

    private void FixedUpdate() => player.FixedUpdate(Time.fixedDeltaTime);

    Vector2 IPositionGetter.Value => ((IPositionGetter)player).Value;

    IPercentable<int> IHealthGetter.Value => ((IHealthGetter)player).Value;

    int IScoreGetter.Value => ((IScoreGetter)player).Value;

    event Action IScoreNotification.OnScoreChanged
    {
        add => ((IScoreNotification)player).OnScoreChanged += value;
        remove => ((IScoreNotification)player).OnScoreChanged -= value;
    }

    event Action IDeath.OnDied
    {
        add => ((IDeath)player).OnDied += value;
        remove => ((IDeath)player).OnDied -= value;
    }

    void IScoreCollector.Add(int amount) => ((IScoreCollector)player).Add(amount);

    void IDamageTaker.TakeDamage(uint damage) => ((IDamageTaker)player).TakeDamage(damage);
}
