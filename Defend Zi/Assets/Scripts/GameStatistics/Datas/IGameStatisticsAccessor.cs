using System;

public interface IGameStatisticsAccessor
{
    TimeSpan TotalInAppTime { get; }
    TimeSpan TotalLifeTime { get; }
    uint GamesNumber { get; }
    uint BestScore { get; }
    TimeSpan BestLifeTime { get; }
}
