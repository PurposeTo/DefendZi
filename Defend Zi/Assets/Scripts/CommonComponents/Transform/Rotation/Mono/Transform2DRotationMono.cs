using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[DisallowMultipleComponent]
public class Transform2DRotationMono : MonoBehaviourExt, IRotation
{
    private IRotation _rotation;

    protected override void AwakeExt()
    {
        _rotation = new Transform2DRotation(transform);
    }

    float IRotationAccessor.Angle => _rotation.Angle;

    Quaternion IRotationAccessor.Quaternion => _rotation.Quaternion;

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion)
    {
        _rotation.RotateBy(deltaQuaternion);
    }

    void IMoveRotation.RotateTo(Quaternion finalQuaternion)
    {
        _rotation.RotateTo(finalQuaternion);
    }
}
