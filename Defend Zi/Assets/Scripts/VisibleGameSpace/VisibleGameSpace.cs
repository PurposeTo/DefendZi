using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Описывает видимую камерой часть игровой плоскости.
/// </summary>
public class VisibleGameSpace : MonoBehaviourExt, IRect2DPointsPosition, IPositionGetter
{
    public const float Height = 15;

    //todo: нужен ли размер?
    //private Rectangle Size => new Rectangle(Vector2.Distance(_leftDownCorner, _rightDownCorner), Vector2.Distance(_leftDownCorner, _leftTopCorner));
    Vector2 IPositionGetter.Value => transform.position;

    [SerializeField, NotNull] private Camera _camera;

    Vector2 IRect2DPointsPosition.LeftDown => _leftDownCorner;
    Vector2 IRect2DPointsPosition.RightDown => _rightDownCorner;
    Vector2 IRect2DPointsPosition.RightTop => _rightTopCorner;
    Vector2 IRect2DPointsPosition.LeftTop => _leftTopCorner;


    private Vector2 _leftDownCorner;
    private Vector2 _rightDownCorner;
    private Vector2 _rightTopCorner;
    private Vector2 _leftTopCorner;
    //глубина относительно положения камеры до искомой плоскости
    private float _depth;

    private void Update()
    {
        UpdateSizePointsPosition();
    }

    private void UpdateSizePointsPosition()
    {
        transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0f);
        _depth = _camera.transform.position.z;

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
