using Desdiene.Extensions.UnityEngine;
using Desdiene.Random;
using UnityEngine;

public class OnePointToPoint : Chunk, ITriggerable
{
    [SerializeField, NotNull] private PointToPointMovementMono _obstacle;

    private ITriggerable TriggerableChunk => _obstacle;

    private readonly float[] spawnRotation = { 0, 90, 180, 270 };

    protected override void OnSpawn()
    {
        _obstacle.transform
            .SetRotation(Quaternion.Euler(0f, 0f, Randomizer.GetRandomItem(spawnRotation)));
    }

    void ITriggerable.Invoke() => TriggerableChunk.Invoke();
}
