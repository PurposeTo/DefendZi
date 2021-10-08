using System;
using Desdiene.DataStorageFactories.Datas;

public interface IGameData : IData
{
    int GamesNumber { get; }
    int BestScore { get; }
    TimeSpan AverageLifeTime { get; }
    TimeSpan BestLifeTime { get; }

    void IncreaseGamesNumber();

    void SetBestScore(uint score);

    void SetBestLifeTime(TimeSpan time);
}
