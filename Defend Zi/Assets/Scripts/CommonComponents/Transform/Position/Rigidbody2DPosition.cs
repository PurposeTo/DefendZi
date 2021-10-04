using System;
using UnityEngine;

public class Rigidbody2DPosition : IPosition
{
    private readonly Rigidbody2D _rigidbody2D;

    public Rigidbody2DPosition(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D != null
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
    }

    Vector2 IPositionAccessor.Value => _rigidbody2D.position;

    public event Action OnChanged;

    void IMovePosition.MoveTo(Vector2 finalPosition)
    {
        MoveTo(finalPosition);
    }

    void IMovePosition.MoveBy(Vector2 deltaDistance)
    {
        MoveTo(_rigidbody2D.position + deltaDistance);
    }

    private void MoveTo(Vector2 finalPosition)
    {
        _rigidbody2D.MovePosition(finalPosition);
        OnChanged?.Invoke();
    }
}
