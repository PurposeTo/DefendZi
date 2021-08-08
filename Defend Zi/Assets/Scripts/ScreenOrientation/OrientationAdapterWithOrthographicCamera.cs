using UnityEngine;

/// <summary>
/// Отдаляет ортографическую камеру
/// </summary>
public class OrientationAdapterWithOrthographicCamera : OrientationAdapter
{
    private float PortraitCameraSize => LandscapeCameraSize * AspectRatio;
    private float LandscapeCameraSize => VisibleGameSpace.Height / 2;

    protected override void ChangeVisionToLandscape()
    {
        Camera.orthographicSize = LandscapeCameraSize;
    }

    protected override void ChangeVisionToPortrait()
    {
        Camera.orthographicSize = PortraitCameraSize;
    }
}
