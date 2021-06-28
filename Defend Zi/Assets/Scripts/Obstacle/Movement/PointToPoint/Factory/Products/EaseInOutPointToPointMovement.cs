using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class EaseInOutPointToPointMovement : PointToPointMovement
{
    public EaseInOutPointToPointMovement(MonoBehaviourExt monoBehaviour,
                                         IPosition position,
                                         Vector2 targetPosition,
                                         float speed)
        : base(monoBehaviour, position, targetPosition, speed, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)) { }
}
