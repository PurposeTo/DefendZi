using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using UnityEngine;

/// <summary>
/// �������, ����������� �������������.
/// </summary>
public class ObstacleSpace : MonoBehaviourExtContainer
{
    public float Width { get; private set; } // ���������� ��������.

    // �������� ����� ������������. ���������� ��������.
    public float ActiveSpaceWidth { get; }
    public Vector2 ActiveSpacePosition { get; }

    private readonly IRandomlySelectableItem<Chunk>[] _selectableChunks;
    private readonly FloatRange _extraSpaceOnGeneration;

    public ObstacleSpace(MonoBehaviourExt mono, ObstacleSpaceData data) : base(mono)
    {
        Width = data.Width;

        _selectableChunks = data.GenerationData.SelectableChunks;
        _extraSpaceOnGeneration = data.GenerationData.ExtraSpaceOnGeneration;

        //todo: ������� ����� ���������������� ���������� ������
        for (int i = 0; i < 300; i++)
        {
            GenerateObstacles();
        }
    }

    /*
     * 1. ��������� ��������� �������/������� VisibleGameSpace.
     * 2. ����� �� ��������� ������� VisibleGameSpace � ��������� ����� + offset � �������, ������� �� ��������� ObstacleSpace?
     * offset - ����� �������� �������� (�������� ��������). 
     * �����, ����� ����������� �������������� �� ����, ��� �������� VisibleGameSpace �� ������� ������������.
     * 3. ���� ��, ������� �����, ���� ������� �� �.2 �� ������ false.
     * 
     */

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
