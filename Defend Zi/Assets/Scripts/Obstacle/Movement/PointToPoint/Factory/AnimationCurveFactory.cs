using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveFactory
{
    public enum CurveType
    {
        Linear,
        EaseInOut
    }

    private static readonly Dictionary<CurveType, AnimationCurve> _curves = new Dictionary<CurveType, AnimationCurve>();

    static AnimationCurveFactory()
    {
        AddEntry(CurveType.Linear, AnimationCurve.Linear(0f, 0f, 1f, 1f));
        AddEntry(CurveType.EaseInOut, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f));
    }

    public static AnimationCurve Get(CurveType movementType)
    {
        return _curves.TryGetValue(movementType, out var value)
            ? value
            : throw new InvalidOperationException($"Value by key {movementType} does not exist in {_curves}");
    }

    private static void AddEntry(CurveType movementType,
                                 AnimationCurve curve)
    {
        if (_curves.ContainsKey(movementType)) throw new InvalidOperationException($"{_curves} already contains {movementType} key.");
        if (curve is null) throw new ArgumentNullException(nameof(curve));

        _curves.Add(movementType, curve);
    }
}
