using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls;
using UnityEngine;
using Zenject;

/// <summary>
/// Описывает поведение частиц "Glowing" на игровой сцене (во время игрово процесса)
/// </summary>
public class GameGlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private GlowingParticle _glowing;
    private ITimeAccessorNotificator _time;

    [Inject]
    private void Constructor(ITime globalTime)
    {
        _time = globalTime ?? throw new System.ArgumentNullException(nameof(globalTime));
    }

    protected override void AwakeExt()
    {
        SubscribeEvents();
    }

    protected override void OnDisableExt()
    {
        UnsubscribeEvents();
    }

    // todo: это число должно вычисляться исходя из скорости передвижения игрока
    private float GlowingSpeedOx => -12;

    private void SubscribeEvents()
    {
        _time.WhenRunning += StartGlowingMotion;
        _time.WhenStopped += StopGlowingMotion;
    }

    private void UnsubscribeEvents()
    {
        _time.WhenRunning -= StartGlowingMotion;
        _time.WhenStopped -= StopGlowingMotion;
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
