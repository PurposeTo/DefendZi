using Desdiene.Random;
using Desdiene.Types.Percents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SelectableChunksDrawable : ISelectableItems<Chunk>
{
    #region Properties for editor drawer

    public static string SelectableChunksFieldName => nameof(_selectableChunks);

    public static float TotalMass;

    #endregion

    [SerializeField] private SelectableChunk[] _selectableChunks;

    private ISelectableItems<Chunk> _selectableChunkItems;

    public SelectableChunksDrawable()
    {
        _selectableChunkItems = new SelectableItems<Chunk>(_selectableChunks);
    }

    Chunk ISelectableItems<Chunk>.GetRandom() => _selectableChunkItems.GetRandom();

    IPercentGetter ISelectableItems<Chunk>.GetChance(ISelectableItem<Chunk> item) => _selectableChunkItems.GetChance(item);

    IEnumerator<Chunk> IEnumerable<Chunk>.GetEnumerator() => _selectableChunkItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _selectableChunkItems.GetEnumerator();
}
