using System;
using UnityEngine;

public class Transform2DRotation : IRotation
{
    private readonly Transform _transform;

    public Transform2DRotation(Transform transform)
    {
        _transform = transform != null
            ? transform
            : throw new ArgumentNullException(nameof(transform));
    }

    float IRotationAccessor.Angle => Angle;

    Quaternion IRotationAccessor.Quaternion => Quaternion;

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _transform.rotation = finalQuaternion;
    }

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _transform.rotation = Quaternion * deltaQuaternion;
    }

    private float Angle => _transform.rotation.eulerAngles.z;
    private Quaternion Quaternion => Quaternion.AngleAxis(Angle, Vector3.forward);
}
