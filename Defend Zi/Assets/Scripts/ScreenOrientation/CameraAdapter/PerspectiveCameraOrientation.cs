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
        if (DefaultOrientation == ScreenOrientation.LandscapeLeft || DefaultOrientation == ScreenOrientation.LandscapeRight)
        {
            _fieldOfViewLandcape = Camera.fieldOfView;
            float fov = Camera.HorizontalToVerticalFieldOfView(_fieldOfViewLandcape, AspectRatio);
            _fieldOfViewPortrait = fov;
        }
        else if (DefaultOrientation == ScreenOrientation.Portrait || DefaultOrientation == ScreenOrientation.PortraitUpsideDown)
        {
            _fieldOfViewPortrait = Camera.fieldOfView;
            float fov = Camera.VerticalToHorizontalFieldOfView(_fieldOfViewLandcape, AspectRatio);
            _fieldOfViewPortrait = fov;
        }
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

    private float VerticalToHorizontal(float fov)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fov * Mathf.Deg2Rad * 0.5f) / AspectRatio) * Mathf.Rad2Deg;
    }
}
