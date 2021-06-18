using System;
using UnityEngine;

public class Obstacle : IDamageDealer, IScoreGetter
{
    private readonly IDamageDealer _damage = new Damage();
    private readonly IScoreGetter _score;

    public Obstacle(int scoreByAvoding)
    {
        _score = new ScoreGetter(scoreByAvoding);
    }

    int IScoreGetter.Value => _score.Value;

    uint IDamageDealer.Value => _damage.Value;
}
