using UnityEngine;

/// <summary>
/// Изменяет ориентацию экрана, вращает ортографическую камеру, отдаляет и смещает её.
/// </summary>
public class OrthographicOrientationAdapter : OrientationAdapter
{
    protected override float PortraitCameraSize => 15f;
    protected override float LandscapeCameraSize => 7.5f;

    protected override void ResizeCamera(float newSize)
    {
        _camera.orthographicSize = newSize;
    }
}
