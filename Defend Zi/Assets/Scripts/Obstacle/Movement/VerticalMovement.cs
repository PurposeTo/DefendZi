using UnityEngine;

public class VerticalMovement : PositionMovement
{
    protected override void Move(float deltaDistance)
    {
        var nextPosition = new Vector2(Position.Value.x, Position.Value.y + deltaDistance);
        Position.MoveBy(nextPosition);
    }
}
