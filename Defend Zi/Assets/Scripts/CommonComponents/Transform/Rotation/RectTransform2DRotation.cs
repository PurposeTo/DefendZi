using System;
using UnityEngine;

public class RectTransform2DRotation : IRotation
{
    private readonly RectTransform _rectTransform;

    public RectTransform2DRotation(RectTransform rectTransform)
    {
        _rectTransform = rectTransform != null
            ? rectTransform
            : throw new ArgumentNullException(nameof(rectTransform));
    }

    float IRotationAccessor.Angle => Angle;

    Quaternion IRotationAccessor.Quaternion => Quaternion;

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _rectTransform.rotation = finalQuaternion;
    }

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _rectTransform.rotation = Quaternion * deltaQuaternion;
    }

    private float Angle => _rectTransform.rotation.eulerAngles.z;
    private Quaternion Quaternion => Quaternion.AngleAxis(Angle, Vector3.forward);
}
