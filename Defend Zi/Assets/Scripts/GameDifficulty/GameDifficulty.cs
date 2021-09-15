using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using UnityEngine;

public class GameDifficulty : MonoBehaviourExt, IPercentAccessor, IPercentNotifier
{
    [SerializeField] private float _gainPerSec = 0.01f;
    private Percent _difficulty = new Percent();
    private string _difficultyDebug; // поле для выведения в дебаг инспектора

    public bool IsMin => _difficulty.IsMin;
    public bool IsMax => _difficulty.IsMax;
    public float Value => _difficulty.Value;

    public event Action OnValueChanged
    {
        add => _difficulty.OnValueChanged += value;
        remove => _difficulty.OnValueChanged -= value;
    }

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
        _difficulty += _gainPerSec * deltaTime;
    }

    public float Get() => _difficulty.Get();

    private void SubscribeEvents()
    {
        _difficulty.OnValueChanged += UpdateDebugDifficulty;
    }

    private void UnsubscribeEvents()
    {
        _difficulty.OnValueChanged -= UpdateDebugDifficulty;
    }

    private void UpdateDebugDifficulty() => _difficultyDebug = GetDebugDifficulty();

    private string GetDebugDifficulty()
    {
        return $"{(float)Math.Round(_difficulty.Get() * 100, 2)}%";
    }
}
