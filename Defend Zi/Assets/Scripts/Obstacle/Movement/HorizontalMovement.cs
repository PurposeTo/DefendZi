﻿using UnityEngine;

public class HorizontalMovement : PositionMovement
{
    protected override void Move(float deltaTime)
    {
        var nextPosition = new Vector2(Position.Value.x + Speed * deltaTime, Position.Value.y);
        Position.MoveTo(nextPosition);
    }
}