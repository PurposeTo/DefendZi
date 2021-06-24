using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public static class PointToPointMovementFactory
{
    private static readonly Dictionary<PointToPointMovementMono.MovementType, Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement>> _movements = new Dictionary<PointToPointMovementMono.MovementType, Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement>>();

    public static void AddCreator(PointToPointMovementMono.MovementType movementType,
                                  Func<MonoBehaviourExt, IPosition, Vector2, float, PointToPointMovement> creator)
    {
        if (_movements.ContainsKey(movementType)) throw new InvalidOperationException($"{_movements} already contains {movementType} key.");
        if (creator is null) throw new ArgumentNullException(nameof(creator));

        _movements.Add(movementType, creator);
    }

    public static PointToPointMovement Get(PointToPointMovementMono.MovementType movementType,
                                           MonoBehaviourExt monoBehaviour,
                                           IPosition position,
                                           Vector2 targetPosition,
                                           float speed)
    {
        return _movements.TryGetValue(movementType, out var value)
            ? value.Invoke(monoBehaviour, position, targetPosition, speed)
            : throw new InvalidOperationException($"Value by key {movementType} does not exist in {_movements}");
    }
}
