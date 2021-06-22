using System;
using UnityEngine;

public class PointToPointMovementMono : PositionMoverMono
{
    private enum MovementType
    {
        Linear,
        NonLinear
    }

    [SerializeField] private Transform _targetTransform;
    [SerializeField] private MovementType _movementType;
    [SerializeField] private AnimationCurve _animationCurve;

    private PointToPointMovement _movement;

    protected override void Constructor()
    {
        base.Constructor();
        InitializeMovement();
    }

    // TODO: Убрать (добавлено для тестирования)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _movement.Enable();
        }
    }

    private void InitializeMovement()
    {
        if (_movementType == MovementType.Linear)
        {
            _movement = new LinearPointToPointMovement(this, Position, _targetTransform.position, Speed);
        }
        else if (_movementType == MovementType.NonLinear)
        {
            _movement = new NonLinearPointToPointMovement(this, Position, _targetTransform.position, _animationCurve, Speed);
        }
        else throw new Exception($"{_movementType} is unkwown type of {typeof(MovementType)}.");
    }
}
