using System;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

public class Blur : IPercent
{
    private const string _blurSizeField = "_Size";
    private const float _minBlurSize = 0f;
    private const float _maxBlurSize = 4f;

    private readonly Shader _shader;
    private readonly IPercent _size;

    public Blur(Shader shader) : this(shader, new Color(1f, 1f, 1f, 1f)) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shader"></param>
    /// <param name="color">Можно параметризовать цвет и затемнение шейдера. Не поддерживает изменение альфа канала. Белый цвет - прозрачный шейдер.</param>
    public Blur(Shader shader, Color color)
    {
        _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        Material = new Material(_shader);
        Material.color = color;
        _size = new FloatInRange(_minBlurSize, new FloatRange(_minBlurSize, _maxBlurSize));
        Set(_size.Value);
    }

    event Action IPercentNotifier.OnChanged
    {
        add => _size.OnChanged += value;
        remove => _size.OnChanged -= value;
    }

    bool IPercentAccessor.IsMin => _size.IsMin;

    bool IPercentAccessor.IsMax => _size.IsMax;

    float IPercentAccessor.Value => _size.Value;

    void IPercentMutator.Set(float value) => Set(value);

    float IPercentMutator.SetAndGet(float percent) => _size.SetAndGet(percent);

    void IPercentMutator.SetMax() => _size.SetMax();

    void IPercentMutator.SetMin() => _size.SetMin();

    public Material Material { get; }

    private void Set(float value)
    {
        _size.Set(value);
        float bluring = Mathf.Lerp(_minBlurSize, _maxBlurSize, value);
        Material.SetFloat(_blurSizeField, bluring);
    }
}
