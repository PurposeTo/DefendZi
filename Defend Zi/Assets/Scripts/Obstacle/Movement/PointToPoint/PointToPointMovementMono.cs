using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono
{
    public enum MovementType
    {
        Linear,
        EaseInOut
    }

    [SerializeField, NotNull] private Transform _target;
    [SerializeField] private MovementType _movementType;

    private PointToPointMovement _movement;

    protected override void Constructor()
    {
        base.Constructor();
        InitMovement();
    }

    public void Enable()
    {
        _movement.Enable();
    }

    private void InitMovement()
    {
        PointToPointMovementIniter.Init();
        _movement = PointToPointMovementFactory.Get(_movementType, this, Position, _target.position, Speed);
    }
}
