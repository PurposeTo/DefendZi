using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleRotationAngle : MonoBehaviourExt
{
    private float _angle;
    private IRotation _rotation;

    protected override void Constructor()
    {
        _rotation = new ObstacleRotation(GetComponent<Rigidbody2D>());
        Rotate();
    }

    private void Rotate()
    {
        _angle = Random.Range(0, 360);
        _rotation.Rotate(_angle);
    }
}
