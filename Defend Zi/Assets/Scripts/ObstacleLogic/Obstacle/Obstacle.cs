using System;
using UnityEngine;

public class Obstacle :
    IDamage,
    IScoreAccessor,
    IPosition,
    IRotation
{
    private readonly IDamage _damage = new Damage();
    private readonly IScoreAccessor _score;
    private readonly IPosition _position;
    private readonly IRotation _rotation;

    public Obstacle(uint scoreByAvoding, Rigidbody2D rigidbody2D)
    {
        _score = new ScorePoints(scoreByAvoding);
        _position = new Rigidbody2DPosition(rigidbody2D);
        _rotation = new Rigidbody2DRotation(rigidbody2D);
    }

    uint IDamage.Value => _damage.Value;

    uint IScoreAccessor.Value => _score.Value;

    Vector2 IPositionAccessor.Value => _position.Value;

    public float Angle => _rotation.Angle;

    public Quaternion Quaternion => _rotation.Quaternion;

    event Action IPositionNotifier.OnChanged
    {
        add => _position.OnChanged += value;
        remove => _position.OnChanged -= value;
    }

    void IMovePosition.MoveBy(Vector2 deltaDistance) => _position.MoveBy(deltaDistance);

    void IMovePosition.MoveTo(Vector2 finalPosition) => _position.MoveTo(finalPosition);

    public void RotateBy(Quaternion deltaQuaternion) => _rotation.RotateBy(deltaQuaternion);

    public void RotateTo(Quaternion finalQuaternion) => _rotation.RotateTo(finalQuaternion);
}
