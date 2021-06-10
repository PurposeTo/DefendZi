using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class CameraSize : MonoBehaviourExt
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;

    private float _depth;

    private void Update()
    {
        _depth = _playerTransform.position.z - _camera.transform.position.z;
    }

    private void OnDrawGizmos()
    {
        Vector3 leftDown = GetLeftDownCorner();
        Vector3 rightDown = GetRightDownCorner();
        Vector3 rightTop = GetRightTopCorner();
        Vector3 leftTop = GetLeftTopCorner();

        Gizmos.DrawLine(leftDown, rightDown);
        Gizmos.DrawLine(rightDown, rightTop);
        Gizmos.DrawLine(rightTop, leftTop);
        Gizmos.DrawLine(leftTop, leftDown);
    }

    public Vector3 GetLeftDownCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(0f, 0f, _depth));
    }

    public Vector3 GetRightDownCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, _depth));
    }

    public Vector3 GetRightTopCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _depth));
    }

    public Vector3 GetLeftTopCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, _depth));
    }
}
