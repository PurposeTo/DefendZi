using UnityEngine;

public class AroundItsAxisMovement : RotationMovement
{
    protected override void Move(float deltaDistance)
    {
        var quaternion = Quaternion.AngleAxis(Rotation.Angle + deltaDistance, Vector3.forward);
        Rotation.RotateTo(quaternion);
    }
}
