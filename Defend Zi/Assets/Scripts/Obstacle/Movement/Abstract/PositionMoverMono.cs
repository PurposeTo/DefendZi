using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class PositionMoverMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;
    [SerializeField, NotNull] private InterfaceComponent<IPosition> _position;

    protected float Speed => _speed;
    protected IPosition Position => _position.Implementation;

    protected override void AwakeExt()
    {
        //fixme быстрофикс пока SerializeField не делает ForceAwake
        GetComponentsInChildren<InterfaceComponent<IPosition>>();
    }

    public void SetSpeed(float speed) => _speed = speed;
}
