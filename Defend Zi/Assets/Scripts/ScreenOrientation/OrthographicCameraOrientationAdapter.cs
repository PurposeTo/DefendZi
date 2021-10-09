using UnityEngine;

/// <summary>
/// Регулирует видимость ортографической камеры
/// </summary>
public class OrthographicCameraOrientationAdapter : CameraOrientationAdapter
{
    private float LandscapeCameraSize => GameSpaceInSight.Height / 2;
    private float PortraitCameraSize => LandscapeCameraSize * AspectRatio;

    protected override void ChangeVisionToLandscape()
    {
        Camera.orthographicSize = LandscapeCameraSize;
    }

    protected override void ChangeVisionToPortrait()
    {
        Camera.orthographicSize = PortraitCameraSize;
    }
}
