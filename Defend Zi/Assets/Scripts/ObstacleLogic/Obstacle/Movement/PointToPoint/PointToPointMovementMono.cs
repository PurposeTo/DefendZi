using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono, ITriggerable
{
    [SerializeField] private AnimationCurveFactory.CurveType _curveType;

    private PointToPointMovement _movement;

    public void Init(IPositionAccessor target)
    {
        var animationCurve = AnimationCurveFactory.Get(_curveType);

        // установка случайной скорости.
        // todo: какая сущность должна заниматься параметризацией этих значений? у некоторых сущностей PositionMoverMono она - константа. У других - Range между двумя случайными значениями. Плюс завадаться она должна из инспектора или из кода?
        float speed = Random.Range(Speed * 0.5f, Speed * 1.5f);
        SetSpeed(speed);

        _movement = new PointToPointMovement(this, Position, target, Speed, animationCurve);
    }

    void ITriggerable.Invoke() => _movement.Move();
}
