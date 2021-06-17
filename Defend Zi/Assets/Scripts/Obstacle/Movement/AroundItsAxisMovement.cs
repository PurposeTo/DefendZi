public class AroundItsAxisMovement : RotationMovement
{
    protected override void Constructor()
    {
        base.Constructor();
        Counter = Rotation.Value;
    }

    protected override void Move(float deltaTime)
    {
        Counter += deltaTime * Speed; 
        Rotation.Rotate(Counter);
    }
}
