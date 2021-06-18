using UnityEngine;
using Desdiene.MonoBehaviourExtension;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class RotationMovement : Movement
{
    protected IRotation Rotation;

    protected override void Constructor()
    {
        Rotation = new Rotation(GetComponent<Rigidbody2D>());
    }

    private void FixedUpdate()
    {
        Rotate(Speed * Time.fixedDeltaTime);
    }

    protected abstract void Rotate(float deltaAngle);
}
