using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;


[RequireComponent(typeof(Transform))]
[DisallowMultipleComponent]
public class Transform2DPositionMono : MonoBehaviourExt, IPosition
{
    private IPosition _position;

    protected override void AwakeExt()
    {
        _position = new Transform2DPosition(transform);
    }

    event Action IPositionNotifier.OnChanged
    {
        add => _position.OnChanged += value;
        remove => _position.OnChanged -= value;
    }

    Vector2 IPositionAccessor.Value => _position.Value;

    void IMovePosition.MoveBy(Vector2 deltaDistance) => _position.MoveBy(deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => _position.MoveTo(finalPosition);
}
