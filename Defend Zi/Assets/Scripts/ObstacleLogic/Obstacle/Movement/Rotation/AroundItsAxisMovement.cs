using UnityEngine;

public class AroundItsAxisMovement : RotatorMono
{
    protected override void FixedUpdateExt()
    {
        var deltaQuaternion = Quaternion.AngleAxis(Speed * Time.fixedDeltaTime, Vector3.forward);
        Rotation.RotateBy(deltaQuaternion);
    }
}
