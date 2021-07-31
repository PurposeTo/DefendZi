using System;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;

public class LevelGeneratorConfig
{
    public IRandomlySelectableItem<Chunk>[] SelectableChunks { get; }
    public FloatRange ExtraSpaceBetweenChunks { get; }

    public LevelGeneratorConfig(IRandomlySelectableItem<Chunk>[] selectableChunks, FloatRange extraSpaceBetweenChunks)
    {
        SelectableChunks = selectableChunks ?? throw new ArgumentNullException(nameof(selectableChunks));
        ExtraSpaceBetweenChunks = extraSpaceBetweenChunks;
    }
}
