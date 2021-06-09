using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ObstacleMono : MonoBehaviourExt, IDamageDealer, IScoreGetter
{
    private Obstacle obstacle;

    protected override void Constructor()
    {
        obstacle = new Obstacle(scoreByAvoding);
    }

    [SerializeField] private int scoreByAvoding = 5;

    int IScoreGetter.Value => ((IScoreGetter)obstacle).Value;

    uint IDamageDealer.Get() => ((IDamageDealer)obstacle).Get();
}

