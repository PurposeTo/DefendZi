using System.Collections;
using UnityEngine;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;

public abstract class PointToPointMovement
{
    protected readonly float Speed;
    protected readonly IPosition Position;
    protected readonly Vector2 TargetPosition;

    private readonly ICoroutine _routineExecutor;

    public PointToPointMovement(MonoBehaviourExt monoBehaviour, IPosition position, Vector2 targetPosition, float speed)
    {
        _routineExecutor = new CoroutineWrap(monoBehaviour);
        Position = position;
        TargetPosition = targetPosition;
        Speed = speed;
    }

    public void Enable()
    {
        _routineExecutor.StartContinuously(Move());
    }

    protected abstract IEnumerator Move();
}
