using UnityEngine;

public class AroundItsAxisMovement : RotationMovement
{
    protected override void Rotate(float deltaAngle)
    {
        var deltaQuaternion = Quaternion.AngleAxis(deltaAngle, Vector3.forward);
        Rotation.RotateBy(deltaQuaternion);
    }
}
