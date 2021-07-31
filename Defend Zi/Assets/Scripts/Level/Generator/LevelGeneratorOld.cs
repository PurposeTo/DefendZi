using System.Linq;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using UnityEngine;
using Zenject;

public class LevelGeneratorOld : MonoBehaviourExt
{
    // Используется два поля, тк unity не умеет работать с интерфейсами через инспектор.
    [SerializeField] private SelectableChunk[] _selectableChunksMono;
    private IRandomlySelectableItem<Chunk>[] _selectableChunks;

    [SerializeField] private float _levelStartPoint = 40f;

    [SerializeField] private FloatRange _extraSpaceBetweenChunks = new FloatRange(5f, 10f);

    private float _levelWidth = 0f;

    private IPositionGetter _playerPosition; //генерировать чанки нужно по мере продвижения игрока
    private CameraSize _cameraSize; //чанки нужно генерировать вне зоны видимости

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _playerPosition = componentsProxy.PlayerPosition;
        _cameraSize = componentsProxy.CameraSize;
        _levelWidth += _levelStartPoint;
        _selectableChunks = _selectableChunksMono.Select(it => it as IRandomlySelectableItem<Chunk>).ToArray();

        //todo: сделать более оптимизированное построение уровня
        for (int i = 0; i < 300; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        Chunk originalChunk = Randomizer.GetRandomItem(_selectableChunks); // todo возможно понадобится originalChunk при реализации генерации уровня. Если нет - сразу привести к интерфейсу.
        IChunkSize chunkSize = originalChunk;

        float extraSpaceBetweenChunks = Random.Range(_extraSpaceBetweenChunks.Min, _extraSpaceBetweenChunks.Max);
        float spawnPointOx = _levelWidth + extraSpaceBetweenChunks + (chunkSize.SpawnPlaceWidth / 2);

        // todo: необходимо где-то явно указать Y и Z уровня.
        Chunk createdChunk = Instantiate(originalChunk, new Vector3(spawnPointOx, 0f, 0f), Quaternion.identity, transform);
        _levelWidth += extraSpaceBetweenChunks + chunkSize.SpawnPlaceWidth;
    }
}
