using System.Collections;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class NonLinearPointToPointMovement : PointToPointMovement
{
    private readonly AnimationCurve _animationCurve;

    public NonLinearPointToPointMovement(MonoBehaviourExt monoBehaviour,
                                         IPosition position,
                                         Vector2 targetPosition,
                                         AnimationCurve animationCurve,
                                         float speed) : base(monoBehaviour, position, targetPosition, speed)
    {
        _animationCurve = animationCurve;
    }

    protected override IEnumerator Move()
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
