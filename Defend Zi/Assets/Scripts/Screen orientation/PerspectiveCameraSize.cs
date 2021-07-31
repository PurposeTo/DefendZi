using UnityEngine;

public class PerspectiveCameraSize : IResizable
{
    private readonly Camera _camera;

    public PerspectiveCameraSize(Camera camera)
    {
        _camera = camera;
    }

    void IResizable.Resize(float newSize)
    {
        _camera.fieldOfView = newSize;
    }
}
