using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleMono : 
    MonoBehaviourExt, 
    IDamageDealer, 
    IScoreGetter
{
    private IScoreGetter _scoreGetter;
    private IDamageDealer _damageDealer;

    protected override void Constructor()
    {
        Obstacle obstacle = new Obstacle(_scoreByAvoding);

        _scoreGetter = obstacle;
        _damageDealer = obstacle;
    }

    [SerializeField] private int _scoreByAvoding = 5;

    int IScoreGetter.Value => _scoreGetter.Value;

    uint IDamageDealer.Value => _damageDealer.Value;
}
