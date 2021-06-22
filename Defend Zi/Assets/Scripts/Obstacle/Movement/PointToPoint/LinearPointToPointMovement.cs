using System.Collections;
using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class LinearPointToPointMovement : PointToPointMovement
{
    public LinearPointToPointMovement(MonoBehaviourExt monoBehaviour,
                                      IPosition position,
                                      Vector2 targetPosition,
                                      float speed) : base(monoBehaviour, position, targetPosition, speed) { }

    protected override IEnumerator Move()
    {
        float counter = 0f;
        var startPosition = Position.Value;
        var wait = new WaitForFixedUpdate();

        while (counter <= 1f)
        {
            Position.MoveTo(Vector2.Lerp(startPosition, TargetPosition, counter));
            counter += Time.fixedDeltaTime * Speed;
            yield return wait;
        }

        Position.MoveTo(TargetPosition);
    }
}
