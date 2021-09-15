using Desdiene.MonoBehaviourExtension;
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
        _globalPause.OnStarted += StartGlowingMotion;
        _globalPause.OnCompleted += StopGlowingMotion;
    }

    private void UnsubscribeEvents()
    {
        _globalPause.OnStarted -= StartGlowingMotion;
        _globalPause.OnCompleted -= StopGlowingMotion;
    }

    private void StartGlowingMotion()
    {
        Debug.Log("КРЯ Start");
        _glowing.SetConstantSpeedOx(GlowingSpeedOx);
    }

    private void StopGlowingMotion()
    {
        Debug.Log("КРЯ Stop");
        _glowing.SetConstantSpeedOx(0f);
    }
}
