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

    void IMovement.MoveTo(Vector2 vector)
    {
        _rigidbody2D.MovePosition(vector);
        OnChanged?.Invoke();
    }
}
