using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PositionMovement : Movement
{
    private protected virtual float Duration { get; } = 2f;

    private protected IPosition Position;

    protected override void Constructor()
    {
        Position = new Position(GetComponent<Rigidbody2D>());
    }
}
