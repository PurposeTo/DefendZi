using System;
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
        PointToPointMovementInitializer.Init();
    }

    // TODO: Убрать (добавлено для тестирования)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitMovement();
            _movement.Enable();
        }
    }

    private void InitMovement()
    {
        _movement = PointToPointMovementFactory.GetMovement(_movementType, this, Position, _target.position, Speed);
    }
}
