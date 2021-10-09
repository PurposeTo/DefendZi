using Desdiene.Random;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectableChunk", menuName = "ScriptableObjects/SelectableChunk")]
public class SelectableChunk : ScriptableObject, ISelectableItem<Chunk>
{
    #region Property for editor drawer
    
    public static string ChanceMassFieldName => nameof(_chanceMass);
    public static string ChancePercentFieldName => nameof(_chancePercent);
    [SerializeField, HideInInspector] private float _chancePercent = 0;
    
    #endregion

    [SerializeField, NotNull] private Chunk _chunk;
    [SerializeField] private uint _chanceMass;

    string ISelectableItem<Chunk>.Name => _chunk.name;
    Chunk ISelectableItem<Chunk>.Item => _chunk;
    uint ISelectableItem<Chunk>.ChanceMass => _chanceMass;
}
