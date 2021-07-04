using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class PositionMoverMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;
    [SerializeField, NotNull] private InterfaceComponent<IPosition> _position;

    protected float Speed => _speed;
    protected IPosition Position => _position.Implementation ?? throw new System.NullReferenceException(nameof(Position));

    public void SetSpeed(float speed) => _speed = speed;
}
