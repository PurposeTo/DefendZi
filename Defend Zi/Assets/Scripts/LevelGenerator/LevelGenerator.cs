using System.Linq;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Range.Positive;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviourExt
{
    [SerializeField] private ChunkSelection[] chunks;

    [SerializeField] private float _levelStartPoint = 40f;

    [SerializeField] private FloatRange _extraSpaceBetweenChunks = new FloatRange(5f, 10f);

    private float _levelLength = 0f;

    private IPositionGetter _playerPosition; //генерировать чанки нужно по мере продвижения игрока
    private CameraSize _cameraSize; //чанки нужно генерировать вне зоны видимости

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _playerPosition = componentsProxy.PlayerPosition;
        _cameraSize = componentsProxy.CameraSize;
        _levelLength += _levelStartPoint;

        //todo: сделать более оптимизированное построение уровня
        for (int i = 0; i < 300; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        Chunk originalChunk = GetRandomChunk();
        float extraSpaceBetweenChunks = Random.Range(_extraSpaceBetweenChunks.Min, _extraSpaceBetweenChunks.Max);
        float spawnPointOx = _levelLength + extraSpaceBetweenChunks + (originalChunk.SpawnPlaceWidth / 2);

        // todo: необходимо где-то явно указать Y и Z уровня.
        Chunk createdChunk = Instantiate(originalChunk, new Vector3(spawnPointOx, 0f, 0f), Quaternion.identity, transform);
        _levelLength += extraSpaceBetweenChunks + createdChunk.SpawnPlaceWidth;
    }

    private Chunk GetRandomChunk()
    {
        int total = (int)chunks.Sum(x => x.ChanceMass);
        int randomChoice = Random.Range(0, total + 1); //случайное число. Границы - включительно.

        int currentCheck = 0; //вычисление текущего шанса выпадения объекта для проверки
        for (int i = 0; i < chunks.Length; i++)
        {
            currentCheck += (int)chunks[i].ChanceMass;

            if (currentCheck == 0) continue;

            if (randomChoice <= currentCheck) //проверяем, это текущий элемент?
            {
                Debug.Log($"Создан чанк \"{chunks[i].name}\" под индексом [{i}]");
                return chunks[i].Chunk;
            }
        }

        throw new System.IndexOutOfRangeException($"Random chunk was not choosing! RandomChoice = {randomChoice}");
    }
}
