using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControl.Scale;
using UnityEngine;
using Zenject;

public class DebugUIController : MonoBehaviourExt
{
    private GlobalTimeScaler globalTimeScaler;

    [Inject]
    public void Constructor(GlobalTimeScaler globalTimeScaler)
    {
        this.globalTimeScaler = globalTimeScaler;
        ICoroutine coroutine = new CoroutineWrap(this);
        globalTimeScaler.OnTimeScaleChanged += SetDebugText;
        coroutine.StartContinuously(UpdateDebug());
    }

    [SerializeField, NotNull] private DebugUIView debugUIView;

    private float TimeScale => globalTimeScaler.TimeScale;

    protected override void OnDestroyExt()
    {
        globalTimeScaler.OnTimeScaleChanged -= SetDebugText;
    }

    private void SetDebugText(float TimeScale)
    {
        int unscaledDeltaTime = Mathf.Clamp(Mathf.RoundToInt(1 / Time.unscaledDeltaTime), 0, int.MaxValue);

        debugUIView.SetText($"TimeScale: {TimeScale}.\n" +
            $"FPS: {unscaledDeltaTime}");
    }

    private IEnumerator UpdateDebug()
    {
        var wait = new WaitForSecondsRealtime(.1f);
        while (true)
        {
            SetDebugText(TimeScale);
            yield return wait;
        }
    }
}
