using System.Collections;
using UnityEngine;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Container;

public abstract class PointToPointMovement : MonoBehaviourExtContainer
{
    protected readonly float Speed;
    protected readonly IPosition Position;
    protected readonly Vector2 TargetPosition;
    protected AnimationCurve _animationCurve;

    private readonly ICoroutine _routineExecutor;

    public PointToPointMovement(MonoBehaviourExt monoBehaviour,
                                IPosition position,
                                Vector2 targetPosition,
                                float speed) : base(monoBehaviour)
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

    protected IEnumerator Move()
    {
        float t;
        float counter = 0f;
        var startPosition = Position.Value;
        var wait = new WaitForFixedUpdate();

        while (counter <= 1f)
        {
            t = _animationCurve.Evaluate(counter);
            Position.MoveTo(Vector2.Lerp(startPosition, TargetPosition, t));
            counter += Time.fixedDeltaTime * Speed;
            yield return wait;
        }

        Position.MoveTo(TargetPosition);
    }
}
