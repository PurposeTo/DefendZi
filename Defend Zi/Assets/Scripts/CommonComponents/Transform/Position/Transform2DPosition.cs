using System;
using UnityEngine;


public class Transform2DPosition : IPosition
{
    private readonly Transform _transform;

    public Transform2DPosition(Transform transform)
    {
        _transform = transform != null
            ? transform
            : throw new ArgumentNullException(nameof(transform));
    }

    private event Action OnChanged;

    event Action IPositionNotifier.OnChanged
    {
        add => OnChanged += value;
        remove => OnChanged -= value;
    }

    Vector2 IPositionAccessor.Value => Position;

    void IMovePosition.MoveBy(Vector2 deltaDistance) => MoveTo((Vector2)Position + deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => MoveTo(finalPosition);

    private Vector3 Position { get => _transform.position; set => _transform.position = value; }

    private void MoveTo(Vector2 finalPosition)
    {
        _transform.position = new Vector3(finalPosition.x, finalPosition.y, Position.z);
        OnChanged?.Invoke();
    }
}
