using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;


[RequireComponent(typeof(Transform))]
[DisallowMultipleComponent]
public class TransformToIPosition : MonoBehaviourExt, IPosition
{
    Vector2 IPositionGetter.Value => transform.position;

    event Action IPositionNotification.OnChanged
    {
        add => OnChanged += value;
        remove => OnChanged -= value;
    }

    private event Action OnChanged;

    void IMovePosition.MoveBy(Vector2 deltaDistance) => MoveTo((Vector2)transform.position + deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => MoveTo(finalPosition);

    private void MoveTo(Vector2 finalPosition)
    {
        transform.position = finalPosition;
        OnChanged?.Invoke();
    }
}
