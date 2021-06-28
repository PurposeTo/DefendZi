using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveFactory
{
    private static readonly Dictionary<PointToPointMovementMono.MovementType, AnimationCurve> _movements = new Dictionary<PointToPointMovementMono.MovementType, AnimationCurve>();

    static AnimationCurveFactory()
    {
        AddEntry(PointToPointMovementMono.MovementType.Linear, AnimationCurve.Linear(0f, 0f, 1f, 1f));
        AddEntry(PointToPointMovementMono.MovementType.EaseInOut, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f));
    }

    public static AnimationCurve Get(PointToPointMovementMono.MovementType movementType)
    {
        return _movements.TryGetValue(movementType, out var value)
            ? value
            : throw new InvalidOperationException($"Value by key {movementType} does not exist in {_movements}");
    }

    private static void AddEntry(PointToPointMovementMono.MovementType movementType,
                                 AnimationCurve curve)
    {
        if (_movements.ContainsKey(movementType)) throw new InvalidOperationException($"{_movements} already contains {movementType} key.");
        if (curve is null) throw new ArgumentNullException(nameof(curve));

        _movements.Add(movementType, curve);
    }
}
