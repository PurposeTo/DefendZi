using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using UnityEngine;

public class GameDifficulty : MonoBehaviourExt, IPercentAccessorNotifier
{
    [SerializeField] private float _gainPerSec = 0.01f;
    private IPercent _difficulty = new Percent();
    private string _difficultyDebug; // поле для выведения в дебаг инспектора

    protected override void AwakeExt()
    {
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        float pastValue = _difficulty.Value;
        _difficulty.Set(pastValue + _gainPerSec * deltaTime);
    }

    event Action IPercentNotifier.OnChanged
    {
        add => _difficulty.OnChanged += value;
        remove => _difficulty.OnChanged -= value;
    }

    bool IPercentAccessor.IsMin => _difficulty.IsMin;
    bool IPercentAccessor.IsMax => _difficulty.IsMax;
    float IPercentAccessor.Value => _difficulty.Value;

    private void SubscribeEvents()
    {
        _difficulty.OnChanged += UpdateDebugDifficulty;
    }

    private void UnsubscribeEvents()
    {
        _difficulty.OnChanged -= UpdateDebugDifficulty;
    }

    private void UpdateDebugDifficulty() => _difficultyDebug = GetDebugDifficulty();

    private string GetDebugDifficulty()
    {
        return $"{(float)Math.Round(_difficulty.Value * 100, 2)}%";
    }
}
