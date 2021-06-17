using System;
using UnityEngine;

public class ObstacleRotation : IRotation
{
    private readonly Rigidbody2D _rigidbody2D;

    public ObstacleRotation(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D != null
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
    }

    float IRotationGetter.Value => _rigidbody2D.rotation;

    void IRotationSetter.Rotate(float angle)
    {
        _rigidbody2D.rotation = angle;
    }
}
