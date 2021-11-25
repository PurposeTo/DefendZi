using System;
using UnityEngine;

public abstract class CameraOrientation
{
    protected readonly ScreenOrientation DefaultOrientation;
    private readonly IScreenOrientation _screenOrientation;
    private readonly Camera _camera;
    private float _aspectRatio;

    protected CameraOrientation(IScreenOrientation screenOrientation,
                                Camera camera,
                                ScreenOrientation defaultOrientation = ScreenOrientation.Landscape)
    {
        _screenOrientation = screenOrientation ?? throw new ArgumentNullException(nameof(screenOrientation));
        _camera = camera != null
            ? camera
            : throw new ArgumentNullException(nameof(camera));
        DefaultOrientation = defaultOrientation;
        _aspectRatio = GetAspectRatio();

        Init();
        SubscribeEvents();

        Change(GetOrientation());
    }

    protected Camera Camera => _camera;
    protected float AspectRatio => _aspectRatio;

    public void Destroy()
    {
        UnsubscribeEvents();
    }
    protected abstract void Init();

    protected abstract void ChangeVisionToLandscape();
    protected abstract void ChangeVisionToPortrait();

    private float GetAspectRatio()
    {
        float width = _camera.pixelWidth;
        float hight = _camera.pixelHeight;

        Desdiene.Math.Compare(ref hight, ref width);
        return width / hight;
    }

    private ScreenOrientation GetOrientation() => _screenOrientation.Get();


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
        _screenOrientation.OnChange += Change;
    }

    private void UnsubscribeEvents()
    {
        _screenOrientation.OnChange -= Change;
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
