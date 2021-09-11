using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class DebugUIController : MonoBehaviourExt
{
    private ICoroutine _updateDebug;

    protected override void AwakeExt()
    {
        _updateDebug = new CoroutineWrap(this);
        _updateDebug.StartContinuously(UpdateDebug());
    }

    [SerializeField, NotNull] private DebugUIView debugUIView;

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
            SetDebugText(Time.timeScale);
            yield return wait;
        }
    }
}
