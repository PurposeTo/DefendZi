using UnityEngine;

public class HorizontalMovement : PositionMoverMono
{
    private void FixedUpdate()
    {
        Debug.Log($"{GetType()}. PreviousPos: {Position.Value.x}, {Position.Value.y}");
        Position.MoveBy(new Vector2(Speed * Time.fixedDeltaTime, 0f));
        Debug.Log($"{GetType()}. NextPos: {Position.Value.x}, {Position.Value.y}");
    }
}
