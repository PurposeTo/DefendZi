using System;
using Desdiene.Random;
using Desdiene.Types.Ranges.Positive;

public class ObstaclesGenerationData
{
    public ISelectableItem<Chunk>[] SelectableChunks { get; }
    public FloatRange SafeSpaceBetweenChunks { get; }
    public float OffsetGeneration { get; }

    public ObstaclesGenerationData(ISelectableItem<Chunk>[] selectableChunks,
                                   FloatRange safeSpaceBetweenChunks,
                                   float offsetGeneration)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        SafeSpaceBetweenChunks = safeSpaceBetweenChunks;
        OffsetGeneration = offsetGeneration;
    }
}
