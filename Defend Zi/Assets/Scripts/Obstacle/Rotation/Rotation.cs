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

    private float Angle => _rigidbody2D.rotation;
    private Quaternion Quaternion => Quaternion.AngleAxis(Angle, Vector3.forward);

    float IRotationGetter.Angle => Angle;

    Quaternion IRotationGetter.Quaternion => Quaternion;

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _rigidbody2D.MoveRotation(finalQuaternion);
    }

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _rigidbody2D.MoveRotation(Quaternion * deltaQuaternion);
    }
}
