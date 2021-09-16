﻿using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Scalers;
using Desdiene.Types.Processes;
using UnityEngine;
using Zenject;

public class GameGlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private GlowingParticle _glowing;
    private IProcessAccessorNotifier _globalPause;

    [Inject]
    private void Constructor(GlobalTimeScaler globalTimeScaler)
    {
        _globalPause = globalTimeScaler ?? throw new System.ArgumentNullException(nameof(globalTimeScaler));
    }

    protected override void AwakeExt()
    {
        SubscribeEvents();
    }

    protected override void OnDisableExt()
    {
        UnsubscribeEvents();
    }

    private float GlowingSpeedOx => -12;

    private void SubscribeEvents()
    {
        _globalPause.OnStarted += StopGlowingMotion;
        _globalPause.OnCompleted += StartGlowingMotion;
    }

    private void UnsubscribeEvents()
    {
        _globalPause.OnStarted -= StopGlowingMotion;
        _globalPause.OnCompleted -= StartGlowingMotion;
    }

    private void StartGlowingMotion()
    {
        _glowing.SetConstantSpeedOx(GlowingSpeedOx);
    }

    private void StopGlowingMotion()
    {
        _glowing.SetConstantSpeedOx(0f);
    }
}