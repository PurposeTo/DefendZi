using Desdiene.DataStorageFactories.Data;

public interface IGameData : IData
{
    int GamesNumber { get; }
    int BestScore { get; }
    int AverageLifeTimeSec { get; }
    int BestLifeTimeSec { get; }

    void IncreaseGamesNumber();

    void SetBestScore(uint score);

    void SetAverageLifeTimeSec(uint timeSec);

    void SetBestLifeTimeSec(uint timeSec);
}
