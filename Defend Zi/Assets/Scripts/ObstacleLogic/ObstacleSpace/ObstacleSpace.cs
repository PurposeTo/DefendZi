using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Область, заполненная препятствиями.
/// </summary>
public class ObstacleSpace : MonoBehaviourExtContainer, IUpdate
{
    private IRectangleIn2DGetter _visibleGameSpace;

    private readonly IRandomlySelectableItem<Chunk>[] _selectableChunks;
    private readonly FloatRange _extraSpaceOnGeneration;
    private readonly float _offsetGeneration;

    // Длина пространства препятствий. Значение эквивалентно местоположению правой границе chunkSpawn.width
    public float Width { get; private set; }

    public ObstacleSpace(MonoBehaviourExt mono, ObstacleSpaceData data, IRectangleIn2DGetter visibleGameSpace) : base(mono)
    {
        Width = data.Width;

        _selectableChunks = data.GenerationData.SelectableChunks;
        _extraSpaceOnGeneration = data.GenerationData.ExtraSpaceOnGeneration;
        _offsetGeneration = data.GenerationData.OffsetGeneration;

        _visibleGameSpace = visibleGameSpace;
    }

    private bool IsNeedToGenerate => Width <= _visibleGameSpace.RightBorder.x + _offsetGeneration;

    void IUpdate.Invoke(float deltaTime)
    {
        while (IsNeedToGenerate)
        {
            GenerateObstacles();
        }
    }

    private void GenerateObstacles()
    {
        Chunk originalChunk = Randomizer.GetRandomItem(_selectableChunks);
        IChunkSize chunkSize = originalChunk;

        float extraSpace = Random.Range(_extraSpaceOnGeneration.Min, _extraSpaceOnGeneration.Max);
        float spawnPointOx = Width + extraSpace + (chunkSize.SpawnPlaceWidth / 2);

        Vector3 spawnPosition = new Vector3(spawnPointOx, 0f, 0f);
        Object.Instantiate(originalChunk, spawnPosition, Quaternion.identity, monoBehaviourExt.transform);
        Width += extraSpace + chunkSize.SpawnPlaceWidth;
    }
}
