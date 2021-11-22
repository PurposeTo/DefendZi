using UnityEngine;

/// <summary>
/// Регулирует видимость ортографической камеры
/// </summary>
public class OrthographicCameraOrientation : CameraOrientation
{
    public OrthographicCameraOrientation(IScreenOrientation screenOrientation, Camera camera)
        : base(screenOrientation, camera) { }

    private float LandscapeCameraSize => GameSpaceInSight.Height / 2;
    private float PortraitCameraSize => LandscapeCameraSize * AspectRatio;

    protected sealed override void ChangeVisionToLandscape()
    {
        Camera.orthographicSize = LandscapeCameraSize;
    }

    protected sealed override void ChangeVisionToPortrait()
    {
        Camera.orthographicSize = PortraitCameraSize;
    }

    protected sealed override void Init() { }
}
