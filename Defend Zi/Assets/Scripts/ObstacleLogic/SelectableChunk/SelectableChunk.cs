using Desdiene.Random;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectableChunk", menuName = "ScriptableObjects/SelectableChunk")]
public class SelectableChunk : ScriptableObject, ISelectableItem<Chunk>
{
    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chanceMass;

    string ISelectableItem<Chunk>.Name => _chunk.name;
    Chunk ISelectableItem<Chunk>.Item => _chunk;
    uint ISelectableItem<Chunk>.ChanceMass => _chanceMass;
}
