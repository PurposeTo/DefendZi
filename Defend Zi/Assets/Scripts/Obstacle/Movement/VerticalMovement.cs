using UnityEngine;

public class VerticalMovement : PositionMoverMono
{
    private void FixedUpdate()
    {
        Position.MoveBy(Vector2.up * Speed * Time.fixedDeltaTime);
    }
}
