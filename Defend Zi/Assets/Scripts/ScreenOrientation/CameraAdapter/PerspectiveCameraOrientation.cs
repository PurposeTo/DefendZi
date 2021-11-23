using UnityEngine;

/// <summary>
/// Регулирует видимость перспективной камеры
/// </summary>
public class PerspectiveCameraOrientation : CameraOrientation
{
    private float _fieldOfViewLandcape;
    private float _fieldOfViewPortrait;

    public PerspectiveCameraOrientation(IScreenOrientation screenOrientation, Camera camera)
    : base(screenOrientation, camera)
    {

    }

    protected override void Init()
    {
        _fieldOfViewLandcape = 60f;
        _fieldOfViewPortrait = 91.49284f;



        // fixme: Не работает в андроид-сборке.

        //float hFov = VerticalToHorizontalFov(Camera.fieldOfView);

        //if (DefaultOrientation == ScreenOrientation.LandscapeLeft || DefaultOrientation == ScreenOrientation.LandscapeRight)
        //{
        //    _fieldOfViewLandcape = Camera.fieldOfView;
        //    _fieldOfViewPortrait = hFov;
        //}
        //else if (DefaultOrientation == ScreenOrientation.Portrait || DefaultOrientation == ScreenOrientation.PortraitUpsideDown)
        //{
        //    _fieldOfViewPortrait = Camera.fieldOfView;
        //    _fieldOfViewLandcape = hFov;
        //}
    }

    protected sealed override void ChangeVisionToLandscape()
    {
        Camera.fieldOfView = _fieldOfViewLandcape;
    }

    protected sealed override void ChangeVisionToPortrait()
    {
        Camera.fieldOfView = _fieldOfViewPortrait;
    }

    private float GetDistanceToPlain()
    {
        float oppositeSide = GameSpaceInSight.Height / 2f;
        float angle = Camera.fieldOfView / 2f;
        float adjacentSide = oppositeSide / Mathf.Tan(Mathf.Deg2Rad * angle);

        return -1f * adjacentSide;
    }

    private float VerticalToHorizontalFov(float fov)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f) / AspectRatio) * Mathf.Rad2Deg;
    }

    private float HorizontalToVerticalFov(float fov)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f) / AspectRatio) * Mathf.Rad2Deg;
    }
}
