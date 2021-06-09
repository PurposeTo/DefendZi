using Desdiene.MonoBehaviourExtention;

public class ObstacleMono : MonoBehaviourExt, IDamageDealer, IScoreGetter
{
    private Obstacle obstacle;

    protected override void Constructor()
    {
        obstacle = new Obstacle();
    }

    int IScoreGetter.Value => ((IScoreGetter)obstacle).Value;

    uint IDamageDealer.Get() => ((IDamageDealer)obstacle).Get();
}

