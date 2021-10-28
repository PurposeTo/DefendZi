using System;

public interface IGameStatisticsMutator
{
    void AddLifeTime(TimeSpan value);
    void IncrementGamesNumber();
    void SetBestLifeTime(TimeSpan value);
    void SetBestScore(uint value);
}
