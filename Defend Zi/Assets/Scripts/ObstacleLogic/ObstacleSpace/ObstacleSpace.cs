using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using UnityEngine;

/// <summary>
/// Область, заполненная препятствиями.
/// </summary>
public class ObstacleSpace : MonoBehaviourExtContainer
{
    public float Width { get; private set; } // изменяемая величина.

    // активная часть пространства. изменяемые величины.
    public float ActiveSpaceWidth { get; }
    public Vector2 ActiveSpacePosition { get; }

    private readonly IRandomlySelectableItem<Chunk>[] _selectableChunks;
    private readonly FloatRange _extraSpaceOnGeneration;

    public ObstacleSpace(MonoBehaviourExt mono, ObstacleSpaceData data) : base(mono)
    {
        Width = data.Width;

        _selectableChunks = data.GenerationData.SelectableChunks;
        _extraSpaceOnGeneration = data.GenerationData.ExtraSpaceOnGeneration;

        //todo: сделать более оптимизированное построение уровня
        for (int i = 0; i < 300; i++)
        {
            GenerateObstacles();
        }
    }

    /*
     * 1. Отследить изменение позиции/размера VisibleGameSpace.
     * 2. Будет ли следующий участок VisibleGameSpace в следующем кадре + offset в позиции, которая не заполнена ObstacleSpace?
     * offset - длина смещения проверки (изменить название). 
     * Нужна, чтобы препятствия генерировались ДО того, как подойдет VisibleGameSpace до пустого пространства.
     * 3. Если да, создать чанки, пока условие из п.2 не станет false.
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
