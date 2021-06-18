using UnityEngine;
using Desdiene.MonoBehaviourExtension;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class RotatorMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;
    protected IRotation Rotation;

    protected override void Constructor()
    {
        Rotation = new Rotation(GetComponent<Rigidbody2D>());
    }
}
