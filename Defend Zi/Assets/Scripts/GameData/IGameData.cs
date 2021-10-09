using System;
using Desdiene.DataStorageFactories.Datas;

public interface IGameData : IData
{
    uint GamesNumber { get; }
    uint BestScore { get; }
    TimeSpan AverageLifeTime { get; }
    TimeSpan BestLifeTime { get; }

    void IncreaseGamesNumber();

    void SetBestScore(uint score);

    void SetBestLifeTime(TimeSpan time);
}
