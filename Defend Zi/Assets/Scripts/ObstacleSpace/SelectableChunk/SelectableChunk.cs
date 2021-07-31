using Desdiene.Random;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectableChunk", menuName = "ScriptableObjects/SelectableChunk")]
public class SelectableChunk : ScriptableObject, IRandomlySelectableItem<Chunk>
{
    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chanceMass;

    string IRandomlySelectableItem<Chunk>.Name => _chunk.name;
    Chunk IRandomlySelectableItem<Chunk>.Item => _chunk;
    uint IRandomlySelectableItem<Chunk>.ChanceMass => _chanceMass;
}
