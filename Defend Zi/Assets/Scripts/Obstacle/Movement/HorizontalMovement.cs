using UnityEngine;

public class HorizontalMovement : PositionMovement
{
    private void FixedUpdate()
    {
        Move(new Vector2(Speed * Time.fixedDeltaTime, 0f));
    }

    protected override void Move(Vector2 deltaDistance)
    {
        Position.MoveBy(deltaDistance);
    }
}
