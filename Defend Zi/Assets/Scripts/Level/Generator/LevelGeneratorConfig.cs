using System;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;

public class ObstaclesGenerationData
{
    public IRandomlySelectableItem<Chunk>[] SelectableChunks { get; }
    public FloatRange ExtraSpaceOnGeneration { get; }
    public float OffsetGeneration { get; }

    public ObstaclesGenerationData(IRandomlySelectableItem<Chunk>[] selectableChunks,
                                   FloatRange extraSpaceGeneration,
                                   float offsetGeneration)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        ExtraSpaceOnGeneration = extraSpaceGeneration;
        OffsetGeneration = offsetGeneration;
    }
}
