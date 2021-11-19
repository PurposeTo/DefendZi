using Desdiene.MonoBehaviourExtension;
using Desdiene.Randoms;
using UnityEngine;

public class OnePointToPoint : Chunk, ITriggerable
{
    [SerializeField, NotNull] private PointToPointMovementMono _obstacle;
    [SerializeField, NotNull] private InterfaceComponent<IPosition> _target;
    private readonly float[] spawnRotation = { 0, 90, 180, 270 };

    protected override void OnSpawn()
    {
        float angle = Randomizer.GetRandomItem(spawnRotation);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        Vector2 toTarget = (Vector2)_obstacle.transform.position - Target.Value;
        Debug.Log($"ÊÐß {(Vector2)_obstacle.transform.position} - {Target.Value} = {toTarget}");
        toTarget = rotation * toTarget;
        Target.MoveTo((Vector2)_obstacle.transform.position + toTarget);

        _obstacle.Init(Target);
    }

    private ITriggerable TriggerableChunk => _obstacle;
    void ITriggerable.Invoke() => TriggerableChunk.Invoke();

    private IPosition Target => _target.Implementation;
}
