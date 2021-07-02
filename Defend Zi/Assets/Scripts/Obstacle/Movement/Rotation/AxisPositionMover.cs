using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class AxisPositionMover : MonoBehaviourExt
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _obstacleLeftBorderDistance;
    [SerializeField] private float _obstacleRightBorderDistance;

    protected override void AwakeExt()
    {
        InitPosition();
    }

    private void InitPosition()
    {
        float positionOx = Random.Range(_obstacleLeftBorderDistance, _obstacleRightBorderDistance);
        _transform.localPosition = new Vector2(positionOx, transform.localPosition.y);
    }
}
