using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class EaseInOutPointToPointMovement : PointToPointMovement
{
    public EaseInOutPointToPointMovement(MonoBehaviourExt monoBehaviour,
                                         IPosition position,
                                         Vector2 targetPosition,
                                         float speed) : base(monoBehaviour, position, targetPosition, speed)
    {
        _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    }

    static EaseInOutPointToPointMovement()
    {
        PointToPointMovementFactory.AddMovementCreator(PointToPointMovementMono.MovementType.EaseInOut, CreateInstance);
    }

    public static void Init()
    {
        Debug.Log($"{typeof(EaseInOutPointToPointMovement)} type was initialized.");
    }

    private static PointToPointMovement CreateInstance(MonoBehaviourExt monoBehaviour,
                                                       IPosition position,
                                                       Vector2 targetPosition,
                                                       float speed)
    {
        return new EaseInOutPointToPointMovement(monoBehaviour, position, targetPosition, speed);
    }
}
