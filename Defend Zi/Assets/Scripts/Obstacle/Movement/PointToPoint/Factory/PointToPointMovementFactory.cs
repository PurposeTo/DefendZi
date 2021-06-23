using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public static class PointToPointMovementFactory
{
    private static readonly Dictionary<PointToPointMovementMono.MovementType, Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement>> _movements = new Dictionary<PointToPointMovementMono.MovementType, Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement>>();

    public static Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement> GetMovementCreator(PointToPointMovementMono.MovementType movementType)
    {
        var value = _movements[movementType];

        return _movements.ContainsValue(value)
            ? value
            : throw new InvalidOperationException($"Value by key {movementType} does not exist in {_movements}");
    }

    public static void AddMovementCreator(PointToPointMovementMono.MovementType movementType, Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement> creator)
    {
        if (_movements.ContainsKey(movementType)) throw new InvalidOperationException($"{_movements} already contains {movementType} key.");

        _movements.Add(movementType, creator);
    }

    public static PointToPointMovement GetMovement(PointToPointMovementMono.MovementType movementType,
                                           MonoBehaviourExt monoBehaviour,
                                           IPosition position,
                                           Vector2 targetPosition,
                                           float speed)
    {
        return _movements[movementType].Invoke(monoBehaviour, position, targetPosition, speed);
    }
}
