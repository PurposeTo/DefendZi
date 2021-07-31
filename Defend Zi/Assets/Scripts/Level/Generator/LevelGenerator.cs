using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using UnityEngine;

public class LevelGenerator : MonoBehaviourExt
{
    private readonly IRandomlySelectableItem<Chunk>[] _selectableChunks;
    private readonly FloatRange _extraSpaceBetweenChunks;
    private readonly ILevelSize _levelSize;

    public LevelGenerator(LevelGeneratorConfig config, ILevelSize levelSize)
    {
        _levelSize = levelSize;
        _selectableChunks = config.SelectableChunks;
        _extraSpaceBetweenChunks = config.ExtraSpaceBetweenChunks;
    }

    /// <summary>
    /// Сгенерировать часть уроня.
    /// </summary>
    /// <returns>Длина сгенерированного уровня.</returns>
    //todo: надо ли как-то задавать, какой длины должна быть сгенерированная часть?
    public float GenerateRandomChunk()
    {
        float generatedLevelWidth = 0;

            Chunk originalChunk = Randomizer.GetRandomItem(_selectableChunks); // todo возможно понадобится originalChunk при реализации генерации уровня. Если нет - сразу привести к интерфейсу.
            IChunkSize chunkSize = originalChunk;

            float extraSpaceBetweenChunks = Random.Range(_extraSpaceBetweenChunks.Min, _extraSpaceBetweenChunks.Max);
            float spawnPointOx = _levelSize.Width + extraSpaceBetweenChunks + (chunkSize.SpawnPlaceWidth / 2);

            // todo: необходимо где-то явно указать Y и Z уровня.
            Vector3 spawnPosition = new Vector3(spawnPointOx, LevelConfig.YAxisStart, LevelConfig.ZAxisStart);
            Instantiate(originalChunk, spawnPosition, Quaternion.identity, transform);
            generatedLevelWidth += extraSpaceBetweenChunks + chunkSize.SpawnPlaceWidth;

        return generatedLevelWidth;
    }
}
