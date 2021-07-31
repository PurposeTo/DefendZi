using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono, ITriggerable
{
    [SerializeField, NotNull] private InterfaceComponent<IPosition> _target;
    [SerializeField] private AnimationCurveFactory.CurveType _curveType;

    private PointToPointMovement _movement;

    protected override void AwakeExt()
    {
        Init();
    }

    void ITriggerable.Invoke() => _movement.Move();

    private void Init()
    {
        var animationCurve = AnimationCurveFactory.Get(_curveType);
        _movement = new PointToPointMovement(this, Position, _target.Implementation, Speed, animationCurve);
    }
}
