using UnityEngine;
using Desdiene.MonoBehaviourExtension;

public class AxisPositionMover : MonoBehaviourExt
{
    [SerializeField] private float _obstacleLeftBorderDistance;
    [SerializeField] private float _obstacleRightBorderDistance;

    protected override void Constructor()
    {
        InitPosition();
    }

    private void InitPosition()
    {
        float positionOx = Random.Range(_obstacleLeftBorderDistance, _obstacleRightBorderDistance);

        foreach (var transform in GetComponentsOnlyInChildren<Transform>())
        {
            transform.localPosition = new Vector2(positionOx, transform.localPosition.y);
        }
    }
}
