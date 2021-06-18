using UnityEngine;

public class VerticalMovement : PositionMoverMono
{
    private void FixedUpdate()
    {
        Debug.Log($"{GetType()}. PreviousPos: {Position.Value.x}, {Position.Value.y}");
        Position.MoveBy(new Vector2(0f, Speed * Time.fixedDeltaTime));
        Debug.Log($"{GetType()}. NextPos: {Position.Value.x}, {Position.Value.y}");
    }
}
