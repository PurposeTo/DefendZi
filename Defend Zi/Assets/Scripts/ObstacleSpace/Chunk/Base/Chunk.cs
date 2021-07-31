using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt, IChunkSize
{
    [SerializeField, Min(0f)] private float _height = 20;

    [SerializeField, Min(0f)] private float _spawnPlaceWidth;
    [SerializeField, Min(0f)] private float _width;

    float IChunkSize.SpawnPlaceWidth => _spawnPlaceWidth;
    float IChunkSize.Width => _width;
    float IChunkSize.Height => _height;

    protected override void AwakeExt()
    {
        OnSpawn();
    }

    protected abstract void OnSpawn();
}
