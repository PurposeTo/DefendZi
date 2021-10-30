using System;
using Desdiene.Randoms;
using Desdiene.Types.Ranges.Positive;

public class ObstaclesGenerationData
{
    public ObstaclesGenerationData(ISelectableItems<Chunk> selectableChunks,
                                   FloatRange safeSpaceBetweenChunks,
                                   float offsetGeneration)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        SafeSpaceBetweenChunks = safeSpaceBetweenChunks;
        OffsetGeneration = offsetGeneration;
    }

    public ISelectableItems<Chunk> SelectableChunks { get; }
    public FloatRange SafeSpaceBetweenChunks { get; }
    public float OffsetGeneration { get; }
}
