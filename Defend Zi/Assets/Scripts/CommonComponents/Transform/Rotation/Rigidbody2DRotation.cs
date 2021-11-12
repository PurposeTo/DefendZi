using System;
using UnityEngine;

public class Rigidbody2DRotation : IRotation
{
    private readonly Rigidbody2D _rigidbody2D;

    public Rigidbody2DRotation(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D != null
            ? rigidbody2D
            : throw new ArgumentNullException(nameof(rigidbody2D));
        Validate();
    }

    float IRotationAccessor.Angle => Angle;

    Quaternion IRotationAccessor.Quaternion => Quaternion;

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _rigidbody2D.MoveRotation(finalQuaternion);
    }

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _rigidbody2D.MoveRotation(Quaternion * deltaQuaternion);
    }

    private float Angle => _rigidbody2D.rotation;
    private Quaternion Quaternion => Quaternion.AngleAxis(Angle, Vector3.forward);

    private void Validate()
    {
        if (!_rigidbody2D.isKinematic)
        {
            Debug.LogWarning("Rigidbody2D must be kinematic");
            _rigidbody2D.isKinematic = true;
        }
    }
}
