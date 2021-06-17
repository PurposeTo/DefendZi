using UnityEngine;

[CreateAssetMenu(fileName = "ChunkSelectionChance", menuName = "ScriptableObjects/ChunkSelectionChance")]
public class ChunkSelectionChance : ScriptableObject
{
    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chance;

    public Chunk Chunk => _chunk;
    public uint Chance => _chance;
}
