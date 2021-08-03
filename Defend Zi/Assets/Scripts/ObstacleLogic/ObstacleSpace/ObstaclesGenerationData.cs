using System;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;

public class ObstaclesGenerationData
{
    public IRandomlySelectableItem<Chunk>[] SelectableChunks { get; }
    public FloatRange SafeSpaceBetweenChunks { get; }
    public float OffsetGeneration { get; }

    public ObstaclesGenerationData(IRandomlySelectableItem<Chunk>[] selectableChunks,
                                   FloatRange safeSpaceBetweenChunks,
                                   float offsetGeneration)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        SafeSpaceBetweenChunks = safeSpaceBetweenChunks;
        OffsetGeneration = offsetGeneration;
    }
}
