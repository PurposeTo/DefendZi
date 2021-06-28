using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public abstract class PositionMoverMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;
    [SerializeField, NotNull] private IPositionComponent _position;

    protected float Speed => _speed;
    protected IPosition Position => _position.Implementation;
}
