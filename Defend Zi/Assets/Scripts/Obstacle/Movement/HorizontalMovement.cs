using UnityEngine;

public class HorizontalMovement : PositionMovement
{
    protected override void Move(float delta)
    {
        var nextPosition = new Vector2(Position.Value.x + delta, Position.Value.y);
        Position.MoveTo(nextPosition);
    }
}
