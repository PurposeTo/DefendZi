using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Rectangles;
using UnityEngine;

/// <summary>
/// Описывает игровую плоскость в поле зрения.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class GameSpaceInSight : MonoBehaviourExt, IRectangleIn2DAccessor
{
    public const float Height = 15;

    [SerializeField, NotNull] private Camera _camera;
    private BoxCollider2D _colliderArea;

    private RectangleIn2D _area;

    float IRectangleAccessor.Height => _area.Height;
    float IRectangleAccessor.Width => _area.Width;
    Vector2 IPivotOffset2DAccessor.Value => _area.PivotOffset;
    Vector2 IPositionAccessor.Value => _area.Position;
    float IRotationAccessor.Angle => _area.Rotation.eulerAngles.z;
    Quaternion IRotationAccessor.Quaternion => _area.Rotation;

    protected override void AwakeExt()
    {
        _colliderArea = GetBoxTrigger2D();
        _area = GetVisibleArea();
    }

    private void Update()
    {
        UpdateArea();
    }

    private void UpdateArea()
    {
        transform.position = GetPosition();
        transform.rotation = GetRotation();
        _area = GetVisibleArea();
        _area.CopyConfigsTo(_colliderArea);
    }

    private RectangleIn2D GetVisibleArea()
    {
        //Расстояние по z от камеры до игровой плоскости
        float distanceToPlane = -_camera.transform.position.z;
        float height = GetHeight(distanceToPlane);
        float width = GetWidth(distanceToPlane);

        return new RectangleIn2D(new Rectangle(height, width), transform.position, transform.rotation);
    }

    private BoxCollider2D GetBoxTrigger2D()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        return boxCollider2D;
    }

    private Vector3 GetPosition()
    {
        return new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0f);
    }

    private Quaternion GetRotation()
    {
        return _camera.transform.rotation;
    }

    private float GetHeight(float distanceToPlane)
    {
        // Ось координат, при использовании данного метода, начинается в левом нижнем углу.
        Vector2 leftDown = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceToPlane));
        Vector2 leftUp = _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, distanceToPlane));

        return Vector2.Distance(leftDown, leftUp);
    }

    private float GetWidth(float distanceToPlane)
    {
        // Ось координат, при использовании данного метода, начинается в левом нижнем углу.
        Vector2 leftDown = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceToPlane));
        Vector2 leftUp = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, distanceToPlane));

        return Vector2.Distance(leftDown, leftUp);
    }
}
