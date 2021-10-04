using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Rectangles;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    [SerializeField, NotNull] ObjectInSightChecker _chunkInSight;
    [SerializeField, NotNull] ObjectInSightChecker _spawnPlaceInSight;
    [SerializeField, NotNull] InterfaceComponent<IRectangleIn2DAccessor> _chunkSize;
    [SerializeField, NotNull] InterfaceComponent<IRectangleIn2DAccessor> _spawnPlaceSize;

    public float SpawnPlaceWidth => _spawnPlaceSize
        .Implementation
        .Width;
    public float Width => _chunkSize
        .Implementation
        .Width;

    protected override void AwakeExt()
    {
        OnSpawn();
        SubscribeEvents();
    }

    protected abstract void OnSpawn();

    private void SubscribeEvents()
    {
        _chunkInSight.OnOutOfSight += Disable;
        OnDisabled += UnsubscribeEvents;
    }

    private void UnsubscribeEvents()
    {
        _chunkInSight.OnOutOfSight -= Disable;
        OnDisabled -= UnsubscribeEvents;
    }

    private void Disable() => Destroy(gameObject);
}
