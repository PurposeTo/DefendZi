using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSelection", menuName = "ScriptableObjects/ChunkSelection")]
public class ChunkSelection : ScriptableObject
{
    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chanceMass;

    public Chunk Chunk => _chunk;
    public uint ChanceMass => _chanceMass;
}
