﻿using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.PercentAsset;
using UnityEngine;

public class GameDifficulty : MonoBehaviourExt, IPercentGetter, IPercentNotifier
{
    [SerializeField] private float _gainPerSec = 0.01f;
    private Percent _difficulty = new Percent();
    private string _difficultyDebug; // поле для выведения в дебаг инспектора

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