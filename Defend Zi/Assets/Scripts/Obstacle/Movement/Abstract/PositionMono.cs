using UnityEngine;
using Desdiene.MonoBehaviourExtension;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PositionMono : MonoBehaviourExt
{
    [SerializeField] private float _speed;

    protected float Speed => _speed;
    protected IPosition Position;

    protected override void Constructor()
    {
        Position = new Position(GetComponent<Rigidbody2D>());
    }
}
