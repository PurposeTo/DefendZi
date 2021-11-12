using System;
using UnityEngine;

public abstract class CameraOrientation
{
    private readonly ScreenOrientationWrap _screenOrientationWrap;
    private readonly Camera _camera;
    private float _aspectRatio;

    protected CameraOrientation(ScreenOrientationWrap screenOrientationWrap, Camera camera)
    {
        _screenOrientationWrap = screenOrientationWrap ?? throw new ArgumentNullException(nameof(screenOrientationWrap));
        _camera = camera != null
            ? camera
            : throw new ArgumentNullException(nameof(camera));

        _aspectRatio = GetAspectRatio();
        Change(_screenOrientationWrap.Get());
        SubscribeEvents();
    }

    protected Camera Camera => _camera;
    protected float AspectRatio => _aspectRatio;

    public void Destroy()
    {
        UnsubscribeEvents();
    }

    protected abstract void ChangeVisionToLandscape();
    protected abstract void ChangeVisionToPortrait();

    private float GetAspectRatio()
    {
        float width = _camera.pixelWidth;
        float hight = _camera.pixelHeight;

        Desdiene.Math.Compare(ref hight, ref width);
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

    private void SubscribeEvents()
    {
        _screenOrientationWrap.OnChange += Change;
    }

    private void UnsubscribeEvents()
    {
        _screenOrientationWrap.OnChange -= Change;
    }

    private void Change(ScreenOrientation screenOrientation)
    {
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
