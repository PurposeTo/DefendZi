using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class ObstacleMono : 
    MonoBehaviourExt, 
    IDamageDealer, 
    IScoreGetter,
    IPosition,
    IRotation
{
    [SerializeField] private int _scoreByAvoding = 5;

    private IScoreGetter _scoreGetter;
    private IDamageDealer _damageDealer;
    private IPosition _position;
    private IRotation _rotation;

    protected override void Constructor()
    {
        Obstacle obstacle = new Obstacle(_scoreByAvoding, GetComponent<Rigidbody2D>());

        _scoreGetter = obstacle;
        _damageDealer = obstacle;
        _position = obstacle;
        _rotation = obstacle;
    }

    uint IDamageDealer.Value => _damageDealer.Value;

    int IScoreGetter.Value => _scoreGetter.Value;

    Vector2 IPositionGetter.Value => _position.Value;

    float IRotationGetter.Angle => _rotation.Angle;

    Quaternion IRotationGetter.Quaternion => _rotation.Quaternion;

    event Action IPositionNotification.OnChanged
    {
        add { _position.OnChanged += value; }
        remove { _position.OnChanged -= value; }
    }

    void IMovePosition.MoveBy(Vector2 deltaDistance) => _position.MoveBy(deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => _position.MoveTo(finalPosition);

    void IMoveRotation.RotateBy(Quaternion deltaQuaternion) => _rotation.RotateBy(deltaQuaternion);

    void IMoveRotation.RotateTo(Quaternion finalQuaternion) => _rotation.RotateTo(finalQuaternion);
}
