using UnityEngine;

public class VerticalMovement : PositionMovement
{
    protected override void Move(float deltaTime)
    {
        if (Counter < Duration)
        {
            Counter += deltaTime;
            var nextPosition = new Vector2(Position.Value.x, Position.Value.y + Speed * deltaTime);
            Position.MoveTo(nextPosition);
        }
    }
}
