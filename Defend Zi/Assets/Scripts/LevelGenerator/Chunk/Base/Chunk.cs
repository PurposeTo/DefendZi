using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    // TODO: заменить на ValueInRange
    [Min(0f)] private float _width;

    [SerializeField, Min(0f)] private float _height = 20;

    // TODO: заменить на Range
    [SerializeField, Min(0f)] private float _minWidth;
    [SerializeField, Min(0f)] private float _maxWidth;

    public float Width => _width;
    public float Height => _height;
    public float MinWidth => _minWidth;
    public float MaxWidth => _maxWidth;

    private void Awake()
    {
        _width = Random.Range(_minWidth, _maxWidth);
    }

    private void Start()
    {
        OnSpawn();
    }

    protected abstract void OnSpawn();
}
