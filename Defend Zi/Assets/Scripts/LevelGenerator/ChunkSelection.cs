using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSelection", menuName = "ScriptableObjects/ChunkSelection")]
public class ChunkSelection : ScriptableObject
{
    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chance;

    public Chunk Chunk => _chunk;
    public uint Chance => _chance;
}
