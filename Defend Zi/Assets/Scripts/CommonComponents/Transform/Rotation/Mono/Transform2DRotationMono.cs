using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[DisallowMultipleComponent]
public class Transform2DRotationMono : MonoBehaviourExt, IRotation
{
    private IRotation _transform2DRotation;

    protected override void AwakeExt()
    {
        _transform2DRotation = new Transform2DRotation(transform);
    }

    float IRotationAccessor.Angle => _transform2DRotation.Angle;

    Quaternion IRotationAccessor.Quaternion => _transform2DRotation.Quaternion;

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _transform2DRotation.RotateBy(deltaQuaternion);
    }

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _transform2DRotation.RotateTo(finalQuaternion);
    }
}
