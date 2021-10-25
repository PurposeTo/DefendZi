using System;

public interface IGameStatisticsAccessorNotifier
{
    TimeSpan TotalLifeTime { get; }
    uint GamesNumber { get; }
    uint BestScore { get; }
    TimeSpan BestLifeTime { get; }
}
