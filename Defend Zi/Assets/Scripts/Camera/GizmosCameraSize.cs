using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[ExecuteInEditMode]
public class GizmosCameraSize : MonoBehaviourExt
{
    [SerializeField, NotNull] private Camera _camera;
    [SerializeField, NotNull] private Transform _playerTransform;

    private Vector3 _leftDownCorner;
    private Vector3 _rightDownCorner;
    private Vector3 _rightTopCorner;
    private Vector3 _leftTopCorner;
    private float _depth;

    private void Update()
    {
        UpdateSize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_leftDownCorner, _rightDownCorner);
        Gizmos.DrawLine(_rightDownCorner, _rightTopCorner);
        Gizmos.DrawLine(_rightTopCorner, _leftTopCorner);
        Gizmos.DrawLine(_leftTopCorner, _leftDownCorner);
    }

    private void UpdateSize()
    {
        _depth = _playerTransform.position.z - _camera.transform.position.z;

        _leftDownCorner = GetLeftDownCorner();
        _rightDownCorner = GetRightDownCorner();
        _rightTopCorner = GetRightTopCorner();
        _leftTopCorner = GetLeftTopCorner();
    }

    private Vector3 GetLeftDownCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(0f, 0f, _depth));
    }

    private Vector3 GetRightDownCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, _depth));
    }

    private Vector3 GetRightTopCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _depth));
    }

    private Vector3 GetLeftTopCorner()
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, _depth));
    }
}
