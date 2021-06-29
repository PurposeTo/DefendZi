using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    // TODO: заменить на ValueInRange
    [Min(0f)] protected float width;

    [SerializeField, Min(0f)] private float _height;

    // TODO: заменить на Range
    [SerializeField, Min(0f)] private float _minWidth;
    [SerializeField, Min(0f)] private float _maxWidth;

    public float Width => width;
    public float Height => _height;
    public float MinWidth => _minWidth;
    public float MaxWidth => _maxWidth;
}
