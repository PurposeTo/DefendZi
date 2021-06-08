using Desdiene.MonoBehaviourExtention;

public class ObstacleMono : MonoBehaviourExt, IDamageDealer, IScore
{
    private Obstacle obstacle;

    protected override void Constructor()
    {
        obstacle = new Obstacle();
    }

    int IScore.Value => ((IScore)obstacle).Value;

    uint IDamageDealer.Get() => ((IDamageDealer)obstacle).Get();
}

