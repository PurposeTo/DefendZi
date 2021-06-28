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

    public static AnimationCurve Get(CurveType curveType)
    {
        return _curves.TryGetValue(curveType, out var value)
            ? value
            : throw new InvalidOperationException($"Value by key {curveType} does not exist in {_curves}");
    }

    private static void AddEntry(CurveType curveType,
                                 AnimationCurve curve)
    {
        if (_curves.ContainsKey(curveType)) throw new InvalidOperationException($"{_curves} already contains {curveType} key.");
        if (curve is null) throw new ArgumentNullException(nameof(curve));

        _curves.Add(curveType, curve);
    }
}
