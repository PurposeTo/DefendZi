using UnityEngine;
/// <summary>
/// Используется для создания базового префаба чанка, от которого можно делать prefab variant других чанков.
/// </summary>
public class ChunkStub : Chunk
{
    protected override void OnSpawn() 
    {
        Debug.LogError($"{GetType().Name} используется только для создания prefab variant! GameObject: {gameObject.name}");
    }
}
