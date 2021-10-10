using Desdiene;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public abstract class CameraOrientation : MonoBehaviourExt
{
    [SerializeField] private Camera _camera;
    private ScreenOrientationWrap _screenOrientationWrap;
    private float _aspectRatio;

    [Inject]
    private void Constructor(ScreenOrientationWrap screenOrientationWrap)
    {
        _screenOrientationWrap = screenOrientationWrap ?? throw new System.ArgumentNullException(nameof(screenOrientationWrap));
    }

    protected override void AwakeExt()
    {
        _aspectRatio = GetAspectRatio();
        Change(_screenOrientationWrap.Get());
        SubscribeEvents();
    }

    protected override void OnDestroyExt() => UnsubscribeEvents();

    protected Camera Camera => _camera;
    protected float AspectRatio => _aspectRatio;

    protected abstract void ChangeVisionToLandscape();
    protected abstract void ChangeVisionToPortrait();

    private float GetAspectRatio()
    {
        float width = _camera.pixelWidth;
        float hight = _camera.pixelHeight;

        Math.Compare(ref hight, ref width);
        return width / hight;
    }

    private void ChangeToLandscape()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        ChangeVisionToLandscape();
    }

    private void ChangeToPortrait()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
        ChangeVisionToPortrait();
    }

    private void SubscribeEvents() => _screenOrientationWrap.OnChange += Change;

    private void UnsubscribeEvents() => _screenOrientationWrap.OnChange -= Change;

    private void Change(ScreenOrientation screenOrientation)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) return;

        if (screenOrientation == ScreenOrientation.LandscapeLeft || screenOrientation == ScreenOrientation.LandscapeRight)
        {
            ChangeToLandscape();
        }
        else if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
        {
            ChangeToPortrait();
        }
    }
}
