using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class RotatorMono : MonoBehaviourExt
{
    private float _speed;
    [SerializeField, NotNull] private IRotationComponent _rotation;

    protected float Speed => _speed;
    protected IRotation Rotation => _rotation.Implementation;

    public void SetSpeed(float speed) => _speed = speed;
}
