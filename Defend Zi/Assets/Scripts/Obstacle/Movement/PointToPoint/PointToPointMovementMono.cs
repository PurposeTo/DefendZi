using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono
{
    [SerializeField, NotNull] private Transform _target;
    [SerializeField] private AnimationCurveFactory.CurveType _curveType;

    private PointToPointMovement _movement;

    protected override void AwakeExt()
    {
        Init();
    }

    // TODO: решить проблему конфликта Awake
    // TODO: убрать метод, используется в тест-режиме
    private void Start()
    {
        Move();
    }

    public void Move() => _movement.Move();

    public void SetTargetLocalPosition(Vector2 position)
    {
        _target.localPosition = position;
    }

    private void Init()
    {
        var animationCurve = AnimationCurveFactory.Get(_curveType);
        _movement = new PointToPointMovement(this, Position, _target.position, Speed, animationCurve);
    }
}
