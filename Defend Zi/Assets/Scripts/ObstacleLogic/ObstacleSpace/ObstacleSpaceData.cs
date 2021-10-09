public class ObstacleSpaceData
{
    public ObstacleSpaceData(float width, ObstaclesGenerationData generationData)
    {
        Width = width;
        GenerationData = generationData;
    }

    public float Width { get; }
    public ObstaclesGenerationData GenerationData { get; }
}
