﻿using System.Collections;
using UnityEngine;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Container;

public abstract class PointToPointMovement : MonoBehaviourExtContainer
{
    private readonly float _speed;
    private readonly IPosition _position;
    private readonly Vector2 _targetPosition;
    private readonly AnimationCurve _animationCurve;

    private readonly ICoroutine _routineExecutor;

    protected PointToPointMovement(MonoBehaviourExt monoBehaviour,
                                IPosition position,
                                Vector2 targetPosition,
                                float speed,
                                AnimationCurve animationCurve)
        : base(monoBehaviour)
    {
        _routineExecutor = new CoroutineWrap(monoBehaviour);
        _position = position;
        _targetPosition = targetPosition;
        _speed = speed;
        _animationCurve = animationCurve;
    }

    public void Enable()
    {
        _routineExecutor.StartContinuously(Move());
    }

    protected IEnumerator Move()
    {
        float t;
        float counter = 0f;
        var startPosition = _position.Value;
        var wait = new WaitForFixedUpdate();

        while (counter <= 1f)
        {
            t = _animationCurve.Evaluate(counter);
            _position.MoveTo(Vector2.Lerp(startPosition, _targetPosition, t));
            counter += Time.fixedDeltaTime * _speed;
            yield return wait;
        }

        _position.MoveTo(_targetPosition);
    }
}