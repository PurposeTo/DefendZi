using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleMono : 
    MonoBehaviourExt, 
    IDamageDealer, 
    IScoreGetter
{
    private Obstacle _obstacle;

    protected override void Constructor()
    {
        _obstacle = new Obstacle(_scoreByAvoding);
    }

    [SerializeField] private int _scoreByAvoding = 5;

    int IScoreGetter.Value => ((IScoreGetter)_obstacle).Value;

    uint IDamageDealer.Value => ((IDamageDealer)_obstacle).Value;
}
