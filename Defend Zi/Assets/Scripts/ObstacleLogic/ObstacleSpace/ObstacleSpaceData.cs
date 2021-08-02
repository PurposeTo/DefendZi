public class ObstacleSpaceData
{
    public float Width { get; }
    public ObstaclesGenerationData GenerationData { get; }

    public ObstacleSpaceData(float width, ObstaclesGenerationData generationData)
    {
        Width = width;
        GenerationData = generationData;
    }
}
