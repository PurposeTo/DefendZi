public class AroundItsAxisMovement : RotationMovement
{
    protected override void Move(float deltaTime)
    {
        Rotation.Rotate(Rotation.Value + deltaTime * Speed);
    }
}
