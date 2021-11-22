using System;
using Desdiene.Types.Percentables;
using Desdiene.Types.Percents;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;

public class Blur : IPercent
{
    private static readonly Color _transparent = Color.white;
    private const string _blurSizeField = "_Size";
    private const float _minBlurSize = 0f;
    private const float _maxBlurSize = 4f;

    private readonly Shader _shader;
    private readonly Color _color;
    private readonly IPercent _effect;

    public Blur(Shader shader) : this(shader, _transparent) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shader"></param>
    /// <param name="color">Можно параметризовать цвет и затемнение шейдера. Не поддерживает изменение альфа канала. Белый цвет - прозрачный шейдер.</param>
    public Blur(Shader shader, Color color)
    {
        _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        Material = new Material(_shader);
        _color = color;
        _effect = new FloatInRange(_minBlurSize, new FloatRange(_minBlurSize, _maxBlurSize));
        OnEffectChanged();
        _effect.OnChanged += OnEffectChanged;
    }

    event Action IPercentNotifier.OnChanged
    {
        add => _effect.OnChanged += value;
        remove => _effect.OnChanged -= value;
    }

    bool IPercentAccessor.IsMin => _effect.IsMin;

    bool IPercentAccessor.IsMax => _effect.IsMax;

    float IPercentAccessor.Value => _effect.Value;

    void IPercentMutator.Set(float value) => _effect.Set(value);

    float IPercentMutator.SetAndGet(float percent) => _effect.SetAndGet(percent);

    void IPercentMutator.SetMax() => _effect.SetMax();

    void IPercentMutator.SetMin() => _effect.SetMin();

    public Material Material { get; }

    private void OnEffectChanged()
    {
        float value = _effect.Value;
        float bluring = Mathf.Lerp(_minBlurSize, _maxBlurSize, value);
        Color color = Color.Lerp(_transparent, _color, value);
        Material.SetFloat(_blurSizeField, bluring);
        Material.color = color;
    }
}
