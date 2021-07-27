using Desdiene;

public class GameData : IGameData
{
    public int MaxScore { get; private set; } = 0;

    public void SetMaxScore(uint score)
    {
        MaxScore = Math.ClampMin((int)score, MaxScore);
    }
}
