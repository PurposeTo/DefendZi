using System;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

public class Blur : IPercent
{
    private const string _blurSizeField = "_Size";
    private const float _minBlurSize = 0f;
    private const float _maxBlurSize = 5f;

    private readonly Shader _shader;
    private readonly IPercent _size;

    public Blur(Shader shader)
    {
        _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        Material = new Material(_shader);
        _size = new FloatInRange(_minBlurSize, new FloatRange(_minBlurSize, _maxBlurSize));
        ((IPercentMutator)this).Set(_size.Value);
    }

    event Action IPercentNotifier.OnChanged
    {
        add => _size.OnChanged += value;
        remove => _size.OnChanged -= value;
    }

    bool IPercentAccessor.IsMin => _size.IsMin;

    bool IPercentAccessor.IsMax => _size.IsMax;

    float IPercentAccessor.Value => _size.Value;

    void IPercentMutator.Set(float value)
    {
        _size.Set(value);
        Material.SetFloat(_blurSizeField, _size.Value);
    }

    float IPercentMutator.SetAndGet(float percent)
    {
        return _size.SetAndGet(percent);
    }

    void IPercentMutator.SetMax()
    {
        _size.SetMax();
    }

    void IPercentMutator.SetMin()
    {
        _size.SetMin();
    }

    public Material Material { get; }
}
