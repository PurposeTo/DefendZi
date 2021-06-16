using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviourExt
{
    [SerializeReference] private List<Chunk> chunks;

    private float levelLength = 0f;

    private IPositionGetter playerPosition; //генерировать чанки нужно по мере продвижения игрока
    private CameraSize cameraSize; //чанки нужно генерировать вне зоны видимости

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        playerPosition = componentsProxy.PlayerPosition;
        cameraSize = componentsProxy.CameraSize;
    }

    private void AddChunk()
    {
        Chunk originalChunk = GetRandomChunk();
        float creatingPoint = levelLength + (originalChunk.Width / 2);

        // todo: необходимо где-то явно указать Y и Z уровня.
        Chunk createdChunk = Instantiate(originalChunk, new Vector3(creatingPoint, 0f, 0f), Quaternion.identity);
        levelLength += originalChunk.Width;
    }

    private Chunk GetRandomChunk()
    {
        return chunks[Random.Range(0, chunks.Count)];
    }
}
