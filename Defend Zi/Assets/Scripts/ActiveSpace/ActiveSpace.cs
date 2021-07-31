using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Концепция состоит в том, что активное игровое пространство существует только в контексте ощущений пользователя.
/// В данном случае это:
///  - Игрок
///  - Взаимодействия с игроком со стороны пользователя и со стороны игрового мира
///  - То, что видит камера.
///  
/// Для упрощения - область видимости камеры это активное игровое пространство.
///  
/// Существует также пассивное игровое пространство, просчеты физики/математики в котором упрощаются.
/// Пассивное игровое пространство - все то, что находится за пределом активного.
/// </summary>
public class ActiveSpace : MonoBehaviourExt, IRect2DPointsPosition, ITransform2DGetter
{
    public const float Height = 15;

    Vector2 ITransform2DGetter.Position => transform.position;
    Quaternion ITransform2DGetter.Rotation => transform.rotation;

    Rectangle ITransform2DGetter.Size => new Rectangle(Vector2.Distance(_leftDownCorner, _rightDownCorner),
                                                 Vector2.Distance(_leftDownCorner, _leftTopCorner));
    Vector2 ITransform2DGetter.Pivot => transform.position;

    [SerializeField, NotNull] private Camera _camera;
    [SerializeField, NotNull] private Transform _playerTransform;

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
