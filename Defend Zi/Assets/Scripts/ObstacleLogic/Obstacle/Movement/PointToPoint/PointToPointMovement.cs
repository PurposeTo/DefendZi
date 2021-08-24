using System.Collections;
using Desdiene.Container;
using Desdiene.CoroutineWrapper;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PointToPointMovement : MonoBehaviourExtContainer
{
    private readonly float _speed;
    private readonly IPosition _position;
    private readonly IPositionGetter _targetPosition;
    private readonly AnimationCurve _animationCurve;
    private readonly ICoroutine _routineExecutor;

    public PointToPointMovement(MonoBehaviourExt monoBehaviour,
                                IPosition position,
                                IPositionGetter targetPosition,
                                float speed,
                                AnimationCurve animationCurve)
        : base(monoBehaviour)
    {
        _routineExecutor = new CoroutineWrap(monoBehaviour);
        _position = position ?? throw new System.ArgumentNullException(nameof(position));
        _targetPosition = targetPosition;
        _speed = speed;
        _animationCurve = animationCurve ?? throw new System.ArgumentNullException(nameof(animationCurve));
    }

    public void Move()
    {
        _routineExecutor.StartContinuously(MoveEnumerator());
    }

    private IEnumerator MoveEnumerator()
    {
        float t;
        float counter = 0f;
        var startPosition = _position.Value;
        var wait = new WaitForFixedUpdate();

        while (counter <= 1f)
        {
            t = _animationCurve.Evaluate(counter);
            _position.MoveTo(Vector2.Lerp(startPosition, _targetPosition.Value, t));
            counter += Time.fixedDeltaTime * _speed;
            yield return wait;
        }

        _position.MoveTo(_targetPosition.Value);
    }
}
