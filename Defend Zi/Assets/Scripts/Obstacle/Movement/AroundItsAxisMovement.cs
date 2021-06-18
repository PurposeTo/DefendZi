public class AroundItsAxisMovement : RotationMovement
{
    protected override void Move(float delta)
    {
        Rotation.RotateTo(Rotation.Angle + delta);
    }
}
