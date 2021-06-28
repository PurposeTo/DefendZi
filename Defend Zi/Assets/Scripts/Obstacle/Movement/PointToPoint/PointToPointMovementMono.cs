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
        Init();
    }

    // TODO: решить проблему конфликта Awake
    private void Start()
    {
        Move();
    }

    public void Move()
    {
        _movement.Move();
    }

    private void Init()
    {
        var animationCurve = AnimationCurveFactory.Get(_movementType);
        _movement = new PointToPointMovement(this, Position, _target.position, Speed, animationCurve);
    }
}
