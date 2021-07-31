using UnityEngine;

public class OrthographicCameraSize : IResizable
{
    private readonly Camera _camera;

    public OrthographicCameraSize(Camera camera)
    {
        _camera = camera;
    }

    void IResizable.Resize(float newSize)
    {
        _camera.orthographicSize = newSize;
    }
}
