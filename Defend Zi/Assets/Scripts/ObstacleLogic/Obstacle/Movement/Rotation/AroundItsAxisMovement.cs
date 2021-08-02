using UnityEngine;

public class AroundItsAxisMovement : RotatorMono
{
    private void FixedUpdate()
    {
        var deltaQuaternion = Quaternion.AngleAxis(Speed * Time.fixedDeltaTime, Vector3.forward);
        Rotation.RotateBy(deltaQuaternion);
    }
}
