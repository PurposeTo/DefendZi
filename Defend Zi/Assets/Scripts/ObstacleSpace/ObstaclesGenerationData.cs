using System;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;

public class ObstaclesGenerationData
{
    public IRandomlySelectableItem<Chunk>[] SelectableChunks { get; }
    public FloatRange ExtraSpaceOnGeneration { get; }

    public ObstaclesGenerationData(IRandomlySelectableItem<Chunk>[] selectableChunks, FloatRange extraSpaceGeneration)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        ExtraSpaceOnGeneration = extraSpaceGeneration;
    }
}
