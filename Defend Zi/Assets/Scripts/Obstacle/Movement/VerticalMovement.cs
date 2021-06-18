using UnityEngine;

public class VerticalMovement : PositionMovement
{
    protected override void Move(float delta)
    {
        var nextPosition = new Vector2(Position.Value.x, Position.Value.y + delta);
        Position.MoveTo(nextPosition);
    }
}
