using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class ObstacleMono :
    MonoBehaviourExt,
    IDamage,
    IScoreAccessor,
    IPosition,
    IRotation
{
    [SerializeField] private uint _scoreByAvoding = 5;

    private IScoreAccessor _scoreAccessor;
    private IDamage _damageDealer;
    private IPosition _position;
    private IRotation _rotation;

    protected override void AwakeExt()
    {
        Obstacle obstacle = new Obstacle(_scoreByAvoding, GetComponent<Rigidbody2D>());

        _scoreAccessor = obstacle;
        _damageDealer = obstacle;
        _position = obstacle;
        _rotation = obstacle;
    }

    uint IDamage.Value => _damageDealer.Value;

    uint IScoreAccessor.Value => _scoreAccessor.Value;

    Vector2 IPositionAccessor.Value => _position.Value;

    float IRotationAccessor.Angle => _rotation.Angle;

    Quaternion IRotationAccessor.Quaternion => _rotation.Quaternion;

    event Action IPositionNotifier.OnChanged
    {
        add { _position.OnChanged += value; }
        remove { _position.OnChanged -= value; }
    }

    void IMovePosition.MoveBy(Vector2 deltaDistance) => _position.MoveBy(deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => _position.MoveTo(finalPosition);

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion) => _rotation.RotateBy(deltaQuaternion);

    void IMoveRotation.RotateTo(Quaternion finalQuaternion) => _rotation.RotateTo(finalQuaternion);
}
