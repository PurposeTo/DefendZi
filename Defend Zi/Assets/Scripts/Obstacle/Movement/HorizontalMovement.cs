﻿using UnityEngine;

public class HorizontalMovement : PositionMoverMono
{
    private void FixedUpdate()
    {
        Debug.Log($"{GetType()}. PreviousPos: {Position.Value.x}, {Position.Value.y}");
        Position.MoveBy(Vector2.right * Speed * Time.fixedDeltaTime);
        Debug.Log($"{GetType()}. NextPos: {Position.Value.x}, {Position.Value.y}");
    }
}
