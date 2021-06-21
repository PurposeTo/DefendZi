using UnityEngine;

public class HorizontalMovement : PositionMoverMono
{
    private void FixedUpdate()
    {
        Position.MoveBy(Vector2.right * Speed * Time.fixedDeltaTime);
    }
}
