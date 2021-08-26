using System.Linq;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Ranges.Positive;
using Desdiene.Types.Rectangles;
using UnityEngine;

/// <summary>
/// �������, ����������� �������������.
/// </summary>
public class ObstacleSpace : MonoBehaviourExtContainer, IUpdate
{
    private IRectangleIn2DGetter _visibleGameSpace;
    private IPositionGetter _visibleGameSpacePosition;

    private readonly ISelectableItem<Chunk>[] _selectableChunks;
    private readonly FloatRange _safeSpaceBetweenChunks;
    private readonly float _offsetGeneration;

    // ����� ������������ �����������. �������� ������������ �������������� ������ ������� chunkSpawn.width
    public float Width { get; private set; }

    public ObstacleSpace(MonoBehaviourExt mono, ObstacleSpaceData data, IRectangleIn2DGetter visibleGameSpace) : base(mono)
    {
        Width = data.Width;

        _selectableChunks = data.GenerationData.SelectableChunks;
        _safeSpaceBetweenChunks = data.GenerationData.SafeSpaceBetweenChunks;
        _offsetGeneration = ValidateOffsetGeneration(data.GenerationData.OffsetGeneration);

        _visibleGameSpace = visibleGameSpace;
        _visibleGameSpacePosition = visibleGameSpace;
    }

    private bool IsNeedToGenerate => Width <= _visibleGameSpacePosition.Value.x + _offsetGeneration;

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

        float safeSpace = Random.Range(_safeSpaceBetweenChunks.Min, _safeSpaceBetweenChunks.Max);
        float spawnPointOx = Width + safeSpace + (originalChunk.SpawnPlaceWidth / 2);

        Vector3 spawnPosition = new Vector3(spawnPointOx, 0f, 0f);
        Object.Instantiate(originalChunk, spawnPosition, Quaternion.identity, monoBehaviourExt.transform);
        Width += safeSpace + originalChunk.SpawnPlaceWidth;
    }

    /// <summary>
    /// ������ ��������� �� ��������� ������ ������������ �����.
    /// ������� ����� ��������� ����� ������, ����� ���� �������������, � ������� ����, � �������, ��������� ����� ������.
    /// </summary>
    private float ValidateOffsetGeneration(float offsetGeneration)
    {
        float maxChunkSpawnPlaceWidth = _selectableChunks.Select(chunk => chunk.Item.Width).Max();
        if (offsetGeneration < maxChunkSpawnPlaceWidth)
        {
            Debug.LogWarning($"offsetGeneration �� ����� ���� ������ ������ ������ �������� ����� - {maxChunkSpawnPlaceWidth}!");
            return maxChunkSpawnPlaceWidth;
        }
        else return offsetGeneration;
    }
}
