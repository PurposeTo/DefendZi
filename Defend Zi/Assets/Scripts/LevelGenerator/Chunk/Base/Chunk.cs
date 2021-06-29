using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    [Min(0f)] protected float Width;
    [SerializeField] [Min(0f)] private float _height;
    [SerializeField] [Min(0f)] private float _minWidth;
    [SerializeField] [Min(0f)] private float _maxWidth;

    public float Height => _height;
    public float MinWidth => _minWidth;
    public float MaxWidth => _maxWidth;

    public float GetWidth() => Width;
}
