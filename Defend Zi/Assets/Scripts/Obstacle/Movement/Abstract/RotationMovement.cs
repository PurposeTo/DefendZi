using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class RotationMovement : Movement
{
    private protected IRotation Rotation;

    protected override void Constructor()
    {
        Rotation = new Rotation(GetComponent<Rigidbody2D>());
    }
}
