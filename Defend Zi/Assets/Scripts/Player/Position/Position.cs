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

    void IMovement.MoveTo(Vector2 vector)
    {
        _rigidbody2D.MovePosition(vector);
        OnChanged?.Invoke();
    }
}
