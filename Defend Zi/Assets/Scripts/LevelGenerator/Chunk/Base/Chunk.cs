using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Desdiene;

public abstract class Chunk : MonoBehaviourExt
{
    [SerializeField] private float _height;
    [SerializeField] private float _width;
    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;

    public float Height => _height;
    public float Width => _width;
    public float MinWidth => _minWidth;
    public float MaxWidth => _maxWidth;

    protected override void Constructor()
    {
        Init();
    }

    private void Init()
    {
        _height = Mathf.Abs(_height);
        _width = Mathf.Abs(_width);
        _minWidth = Mathf.Abs(_minWidth);
        _maxWidth = Mathf.Abs(_maxWidth);

        if (_maxWidth < _minWidth) Randomizer.Swap(ref _maxWidth, ref _minWidth);

        _width = Mathf.Clamp(_width, _minWidth, _maxWidth);
    }
}
