using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls;
using Desdiene.Types.Percents;
using UnityEngine;
using Zenject;

/// <summary>
/// Описывает поведение частиц "Glowing" на игровой сцене (во время игрово процесса)
/// </summary>
public class GameGlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private GlowingParticle _glowing;
    private ITimeAccessorNotificator _time;
    private IPercentAccessorNotifier _gameDifficulty;

    [Inject]
    private void Constructor(ITime globalTime, GameDifficulty gameDifficulty)
    {
        _time = globalTime ?? throw new ArgumentNullException(nameof(globalTime));
        _gameDifficulty = gameDifficulty ?? throw new ArgumentNullException(nameof(gameDifficulty));
    }

    protected override void AwakeExt()
    {
        SetParticlesVelocity();
        SubscribeEvents();
    }

    protected override void OnDisableExt()
    {
        UnsubscribeEvents();
    }

    // todo: это число должно вычисляться
    private Vector2 CameraVelocity => Vector2.Lerp(new Vector2(12, 0), new Vector2(18, 0), _gameDifficulty.Value);

    private void SubscribeEvents()
    {
        _time.OnChanged += SetParticlesVelocity;
        _gameDifficulty.OnChanged += SetParticlesVelocity;
    }

    private void UnsubscribeEvents()
    {
        _time.OnChanged -= SetParticlesVelocity;
        _gameDifficulty.OnChanged -= SetParticlesVelocity;
    }

    private void SetParticlesVelocity()
    {
        Vector2 velocity = CameraVelocity * _time.Scale;
        _glowing.SetConstantVelocity(velocity * -1);
    }
}
