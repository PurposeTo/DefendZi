using System;
using Desdiene.Types.Percents;
using UnityEngine;

public class CanvasGroupAsPercentAlpha : IPercent
{
    private const float _min = 0f;
    private const float _max = 1f;

    private readonly CanvasGroup _canvasGroup;

    public CanvasGroupAsPercentAlpha(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup != null
            ? canvasGroup
            : throw new ArgumentNullException(nameof(canvasGroup));
    }

    private event Action OnChanged;

    bool IPercentAccessor.IsMin => Mathf.Approximately(_min, Alpha);

    bool IPercentAccessor.IsMax => Mathf.Approximately(_max, Alpha);

    float IPercentAccessor.Value => Alpha;

    event Action IPercentNotifier.OnChanged
    {
        add => OnChanged += value;
        remove => OnChanged -= value;
    }

    void IPercentMutator.Set(float percent) => Set(percent);

    float IPercentMutator.SetAndGet(float percent)
    {
        Set(percent);
        return Alpha;
    }

    void IPercentMutator.SetMax() => Alpha = _max;

    void IPercentMutator.SetMin() => Alpha = _min;

    private float Alpha
    {
        get => _canvasGroup.alpha;
        set { _canvasGroup.alpha = value; }
    }

    private void Set(float value)
    {
        if (Mathf.Approximately(value, Alpha)) return;

        Alpha = value;
        OnChanged?.Invoke();
    }
}
