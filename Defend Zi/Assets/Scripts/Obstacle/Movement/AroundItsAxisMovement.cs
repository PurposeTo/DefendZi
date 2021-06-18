using UnityEngine;

public class AroundItsAxisMovement : RotationMono
{
    private void FixedUpdate()
    {
        var deltaQuaternion = Quaternion.AngleAxis(Speed * Time.fixedDeltaTime, Vector3.forward);
        Rotation.RotateBy(deltaQuaternion);
    }
}
