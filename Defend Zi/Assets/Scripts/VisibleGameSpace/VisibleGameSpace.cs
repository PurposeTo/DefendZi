using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Описывает видимую камерой часть игровой плоскости.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class VisibleGameSpace : MonoBehaviourExt, IRectangleIn2DSpace
{
    public const float Height = 15;

    [SerializeField, NotNull] private Camera _camera;
    private BoxCollider2D colliderArea;

    private Rectangle _size;
    private Vector2 _leftDownCorner;
    private Vector2 _rightDownCorner;
    private Vector2 _rightTopCorner;
    private Vector2 _leftTopCorner;

    float IRectangle.Height => _size.Height;
    float IRectangle.Width => _size.Width;

    Vector2 IRectangleIn2DSpace.LeftBorder => new Vector2(_leftDownCorner.x, transform.position.y);
    Vector2 IRectangleIn2DSpace.RightBorder => new Vector2(_rightDownCorner.x, transform.position.y);
    Vector2 IRectangleIn2DSpace.BottomBorder => new Vector2(transform.position.x, _leftDownCorner.y);
    Vector2 IRectangleIn2DSpace.UpperBorder => new Vector2(transform.position.x, _leftTopCorner.y);

    Vector2 IPositionGetter.Value => transform.position;
    Vector2 IRectangleIn2DSpace.LeftDown => _leftDownCorner;
    Vector2 IRectangleIn2DSpace.RightDown => _rightDownCorner;
    Vector2 IRectangleIn2DSpace.RightTop => _rightTopCorner;
    Vector2 IRectangleIn2DSpace.LeftTop => _leftTopCorner;

    protected override void AwakeExt()
    {
        colliderArea = GetBoxTrigger2D();
    }

    private void Update()
    {
        UpdatePosition();
        UpdateSizePointsPosition();
    }

    private BoxCollider2D GetBoxTrigger2D()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        return boxCollider2D;
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0f);
    }

    private void UpdateSizePointsPosition()
    {
        //Расстояние по z от камеры до игровой плоскости
        float distanceToPlane = -_camera.transform.position.z;

        _leftDownCorner = GetLeftDownCorner(distanceToPlane);
        _rightDownCorner = GetRightDownCorner(distanceToPlane);
        _rightTopCorner = GetRightTopCorner(distanceToPlane);
        _leftTopCorner = GetLeftTopCorner(distanceToPlane);

        float height = Vector2.Distance(_leftDownCorner, _leftTopCorner);
        float width = Vector2.Distance(_leftDownCorner, _rightDownCorner);
        _size = new Rectangle(height, width);
        colliderArea.size = new Vector2(_size.Width, _size.Height);
    }

    private Vector3 GetLeftDownCorner(float distanceToPlane)
    {
        // Ось координат, при использовании данного метода, начинается в левом нижнем углу.
        return _camera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceToPlane));
    }

    private Vector3 GetRightDownCorner(float distanceToPlane)
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, distanceToPlane));
    }

    private Vector3 GetRightTopCorner(float distanceToPlane)
    {
        return _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, distanceToPlane));
    }

    private Vector3 GetLeftTopCorner(float distanceToPlane)
    {
        return _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, distanceToPlane));
    }
}
