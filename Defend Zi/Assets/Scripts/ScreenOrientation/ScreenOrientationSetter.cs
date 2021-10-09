using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class ScreenOrientationSetter : MonoBehaviourExt
{
    private ScreenOrientationAdapter _screenOrientationAdapter;

    [Inject]
    private void Constructor(ScreenOrientationAdapter screenOrientationAdapter)
    {
        _screenOrientationAdapter = screenOrientationAdapter ?? throw new System.ArgumentNullException(nameof(screenOrientationAdapter));
    }

    protected override void AwakeExt()
    {
        Set(_screenOrientationAdapter.Value);
        SubscribeEvents();
    }

    protected override void OnDestroyExt() => UnsubscribeEvents();

    private void SubscribeEvents() => _screenOrientationAdapter.OnChange += Set;

    private void UnsubscribeEvents() => _screenOrientationAdapter.OnChange -= Set;

    private void Set(ScreenOrientation orientation) => Screen.orientation = orientation;
}
