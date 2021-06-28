using System;
using UnityEngine;

public class Position : IPosition
{
    private readonly Rigidbody2D _rigidbody2D;

    public Position(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D != null
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
    }

    Vector2 IPositionGetter.Value => _rigidbody2D.position;

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
