using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono, ITriggerable
{
    [SerializeField] private AnimationCurveFactory.CurveType _curveType;

    private PointToPointMovement _movement;

    public void Init(IPositionAccessor target)
    {
        var animationCurve = AnimationCurveFactory.Get(_curveType);
        _movement = new PointToPointMovement(this, Position, target, Speed, animationCurve);
    }

    void ITriggerable.Invoke() => _movement.Move();
}
