using Desdiene;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public abstract class CameraOrientationAdapter : MonoBehaviourExt
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
        Adapt(_screenOrientationWrap.Get());
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

    private void AdaptToLandscape()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        ChangeVisionToLandscape();
    }

    private void AdaptToPortrait()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
        ChangeVisionToPortrait();
    }

    private void SubscribeEvents() => _screenOrientationWrap.OnChange += Adapt;

    private void UnsubscribeEvents() => _screenOrientationWrap.OnChange -= Adapt;

    private void Adapt(ScreenOrientation screenOrientation)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            AdaptToLandscape();
            return;
        }

        if (screenOrientation == ScreenOrientation.LandscapeLeft || screenOrientation == ScreenOrientation.LandscapeRight)
        {
            AdaptToLandscape();
        }
        else if (screenOrientation == ScreenOrientation.Portrait || screenOrientation == ScreenOrientation.PortraitUpsideDown)
        {
            AdaptToPortrait();
        }
    }
}
