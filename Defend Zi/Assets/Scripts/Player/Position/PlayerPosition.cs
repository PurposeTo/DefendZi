using System;
using UnityEngine;

public class PlayerPosition : IPosition
{
    private readonly Rigidbody2D _rigidbody2D;

    public PlayerPosition(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
    }

    Vector2 IPositionGetter.Value => _rigidbody2D.position;

    public event Action OnChanged;

    void IMovePosition.MoveTo(Vector2 finalPosition)
    {
        _rigidbody2D.MovePosition(finalPosition);
        OnChanged?.Invoke();
    }

    void IMovePosition.MoveBy(Vector2 deltaPosition)
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + deltaPosition);
        OnChanged?.Invoke();
    }
}
