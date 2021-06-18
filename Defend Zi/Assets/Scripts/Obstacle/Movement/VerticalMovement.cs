using UnityEngine;

public class VerticalMovement : PositionMovement
{
    private void FixedUpdate()
    {
        Move(new Vector2(0f, Speed * Time.fixedDeltaTime));
    }

    protected override void Move(Vector2 deltaDistance)
    {
        Position.MoveBy(deltaDistance);
    }
}
