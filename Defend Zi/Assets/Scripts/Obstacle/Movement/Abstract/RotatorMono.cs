using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public abstract class RotatorMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;
    [SerializeField, NotNull] private IRotationComponent _rotation;

    protected float Speed => _speed;
    protected IRotation Rotation => _rotation.Implementation;
}
