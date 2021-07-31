using UnityEngine;

/// <summary>
/// Изменяет ориентацию экрана, вращает перспективную камеру, отдаляет и смещает её.
/// </summary>
public class PerspectiveOrientationAdapter : OrientationAdapter
{
    protected override float PortraitCameraSize => 100f;
    protected override float LandscapeCameraSize => 60f;

    protected override void ResizeCamera(float newSize)
    {
        _camera.fieldOfView = newSize;
    }
}
