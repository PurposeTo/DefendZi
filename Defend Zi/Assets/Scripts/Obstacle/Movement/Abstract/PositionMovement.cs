using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PositionMovement : Movement
{
    protected IPosition Position;

    protected override void Constructor()
    {
        Position = new Position(GetComponent<Rigidbody2D>());
    }
}
