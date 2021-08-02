﻿using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Описывает видимую камерой часть игровой плоскости.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class VisibleGameSpace : MonoBehaviourExt, IRectangleIn2DGetter
{
    public const float Height = 15;

    [SerializeField, NotNull] private Camera _camera;
    private BoxCollider2D _colliderArea;

    private RectangleIn2D _area;

    float IRectangleGetter.Height => _area.Height;
    float IRectangleGetter.Width => _area.Width;

    Vector2 IRectangleIn2DGetter.LeftBorder => _area.LeftBorder;
    Vector2 IRectangleIn2DGetter.RightBorder => _area.RightBorder;
    Vector2 IRectangleIn2DGetter.BottomBorder => _area.BottomBorder;
    Vector2 IRectangleIn2DGetter.UpperBorder => _area.UpperBorder;

    Vector2 IPivotOffset2DGetter.Value => Vector2.zero;
    Vector2 IPositionGetter.Value => transform.position;
    Vector2 IRectangleIn2DGetter.LeftDown => _area.LeftDown;
    Vector2 IRectangleIn2DGetter.RightDown => _area.RightDown;
    Vector2 IRectangleIn2DGetter.RightTop => _area.RightTop;
    Vector2 IRectangleIn2DGetter.LeftTop => _area.LeftTop;

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
        UpdateBoxCollider2DWithRect(_colliderArea, _area);
    }

    // Вынести этот метод и сделать его методом расширения?
    private void UpdateBoxCollider2DWithRect(BoxCollider2D boxCollider2D, RectangleIn2D rectangleIn2D)
    {
        if (boxCollider2D == null)
        {
            throw new System.ArgumentNullException(nameof(boxCollider2D));
        }

        boxCollider2D.size = new Vector2(rectangleIn2D.Width, rectangleIn2D.Height);
        boxCollider2D.offset = rectangleIn2D.PivotOffset;
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