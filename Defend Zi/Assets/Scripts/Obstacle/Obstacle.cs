using System;
using UnityEngine;

public class Obstacle :
    IDamageDealer,
    IScoreGetter,
    IPosition
{
    private readonly IDamageDealer _damage = new Damage();
    private readonly IScoreGetter _score;
    private readonly IPosition _position;

    public Obstacle(int scoreByAvoding, Rigidbody2D rigidbody2D)
    {
        _score = new ScoreGetter(scoreByAvoding);
        _position = new Position(rigidbody2D);
    }

    uint IDamageDealer.Value => _damage.Value;

    int IScoreGetter.Value => _score.Value;

    Vector2 IPositionGetter.Value => _position.Value;

    void IMovePosition.MoveBy(Vector2 deltaDistance)
    {
        _position.MoveBy(deltaDistance);
    }

    void IMovePosition.MoveTo(Vector2 finalPosition)
    {
        _position.MoveTo(finalPosition);
    }

    event Action IPositionNotification.OnChanged
    {
        add => _position.OnChanged += value;
        remove => _position.OnChanged -= value;
    }
}
