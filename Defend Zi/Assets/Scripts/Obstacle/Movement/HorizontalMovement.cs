using UnityEngine;

public class HorizontalMovement : PositionMovement
{
    protected override void Move(float deltaDistance)
    {
        var nextPosition = new Vector2(Position.Value.x + deltaDistance, Position.Value.y);
        Position.MoveBy(nextPosition);
    }
}
