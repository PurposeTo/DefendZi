using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class OnePointToPoint : Chunk, IMovableChunk
{
    [SerializeField, NotNull] private PointToPointMovementMono _obstacle;

    private IMovableChunk MovableChunk => _obstacle;

    private readonly float[] spawnRotation = { 0, 90, 180, 270 };

    protected override void OnSpawn()
    {
        _obstacle.transform
            .SetRotation(Quaternion.Euler(0f, 0f, Desdiene.Randomizer.GetRandomItem(spawnRotation)));
    }

    void IMovableChunk.Move() => MovableChunk.Move();
}
