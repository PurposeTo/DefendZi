using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class LinearPointToPointMovement : PointToPointMovement
{
    public LinearPointToPointMovement(MonoBehaviourExt monoBehaviour,
                                      IPosition position,
                                      Vector2 targetPosition,
                                      float speed) : base(monoBehaviour, position, targetPosition, speed)
    {
        _animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    }

    static LinearPointToPointMovement()
    {
        PointToPointMovementFactory.AddMovementCreator(PointToPointMovementMono.MovementType.Linear, CreateInstance);
    }

    public static void Init()
    {
        Debug.Log($"{typeof(LinearPointToPointMovement)} type was initialized.");
    }

    private static PointToPointMovement CreateInstance(MonoBehaviourExt monoBehaviour,
                                                 IPosition position,
                                                 Vector2 targetPosition,
                                                 float speed)
    {
        return new LinearPointToPointMovement(monoBehaviour, position, targetPosition, speed);
    }
}
