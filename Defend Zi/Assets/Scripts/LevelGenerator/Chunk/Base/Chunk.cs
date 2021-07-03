using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    [SerializeField, Min(0f)] private float _height = 20;

    [SerializeField, Min(0f)] private float _spawnPlaceWidth;
    [SerializeField, Min(0f)] private float _width;

    public float SpawnPlaceWidth => _spawnPlaceWidth;
    public float Width => _width;
    public float Height => _height;

    protected override void AwakeExt()
    {
        OnSpawn();
    }

    protected abstract void OnSpawn();
}
