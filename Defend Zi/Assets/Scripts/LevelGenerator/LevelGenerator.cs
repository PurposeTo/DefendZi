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
        float creatingPoint = levelLength + (originalChunk.GetWidth() / 2);

        // todo: необходимо где-то явно указать Y и Z уровня.
        Chunk createdChunk = Instantiate(originalChunk, new Vector3(creatingPoint, 0f, 0f), Quaternion.identity);
        levelLength += createdChunk.GetWidth();
    }

    private Chunk GetRandomChunk()
    {
        uint total = (uint)chunks.Sum(x => x.Chance);
        uint randomChoice = (uint)Random.Range(0, total);

        uint currentCheck = 0; //вычисление текущего шанса выпадения объекта для проверки
        for (int i = 0; i < chunks.Length; i++)
        {
            currentCheck += chunks[i].Chance;
            if (randomChoice <= currentCheck) //проверяем, это текущий элемент?
            {
                //Это текущий элемент!
                return chunks[i].Chunk; //возвращаем значение
            }
        }

        throw new System.IndexOutOfRangeException("Random chunk was not choosing!");
    }
}
