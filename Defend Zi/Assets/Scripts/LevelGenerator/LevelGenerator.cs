using System.Collections.Generic;
using System.Linq;
using Desdiene;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviourExt
{
    [SerializeField] private ChunkSelection[] chunks;

    [SerializeField] private float levelStartPoint = 40f;
    private float levelLength = 0f;

    private IPositionGetter playerPosition; //генерировать чанки нужно по мере продвижения игрока
    private CameraSize cameraSize; //чанки нужно генерировать вне зоны видимости

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        playerPosition = componentsProxy.PlayerPosition;
        cameraSize = componentsProxy.CameraSize;
        levelLength += levelStartPoint;

        //todo: сделать более оптимизированное построение уровня
        for (int i = 0; i < 1000; i++)
        {
            AddChunk();
        }
    }

    private void AddChunk()
    {
        Chunk originalChunk = GetRandomChunk();
        float creatingPoint = levelLength + (originalChunk.Width / 2);

        // todo: необходимо где-то явно указать Y и Z уровня.
        Chunk createdChunk = Instantiate(originalChunk, new Vector3(creatingPoint, 0f, 0f), Quaternion.identity);
        levelLength += createdChunk.Width;
    }

    private Chunk GetRandomChunk()
    {
        int total = (int)chunks.Sum(x => x.ChanceMass);
        int randomChoice = Random.Range(0, total + 1); //случайное число. Границы - включительно.

        int currentCheck = 0; //вычисление текущего шанса выпадения объекта для проверки
        for (int i = 0; i < chunks.Length; i++)
        {
            currentCheck += (int)chunks[i].ChanceMass;
            if (randomChoice <= currentCheck) //проверяем, это текущий элемент?
            {
                Debug.Log($"Создан чанк {chunks[i].name} под индексом [{i}]");
                return chunks[i].Chunk;
            }
        }

        throw new System.IndexOutOfRangeException("Random chunk was not choosing!");
    }
}
