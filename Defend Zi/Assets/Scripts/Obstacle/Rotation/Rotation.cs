using System;
using UnityEngine;

public class Rotation : IRotation
{
    private readonly Rigidbody2D _rigidbody2D;

    public Rotation(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D != null
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
    }

    float IRotationGetter.Angle => _rigidbody2D.rotation;

    Quaternion IRotationGetter.Quaternion => Quaternion.AngleAxis(_rigidbody2D.rotation, Vector3.forward);

    void IMoveRotation.RotateTo(float angle)
    {
        _rigidbody2D.MoveRotation(angle);
    }
}
