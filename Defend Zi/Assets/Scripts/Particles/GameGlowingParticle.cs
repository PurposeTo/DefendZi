using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls;
using Desdiene.Types.Processes;
using UnityEngine;
using Zenject;

public class GameGlowingParticle : MonoBehaviourExt
{
    [SerializeField, NotNull] private GlowingParticle _glowing;
    private ITimeNotification _timeNotification;

    [Inject]
    private void Constructor(ITime globalTime)
    {
        _timeNotification = globalTime ?? throw new System.ArgumentNullException(nameof(globalTime));
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
        _timeNotification.WhenRunning += StartGlowingMotion;
        _timeNotification.WhenStopped += StopGlowingMotion;
    }

    private void UnsubscribeEvents()
    {
        _timeNotification.WhenRunning -= StartGlowingMotion;
        _timeNotification.WhenStopped -= StopGlowingMotion;
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
