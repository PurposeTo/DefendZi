using Desdiene;

public class GameData : IGameData
{
    public int BestScore { get; private set; } = 0;

    public void SetBestScore(uint score)
    {
        BestScore = Math.ClampMin((int)score, BestScore);
    }
}
