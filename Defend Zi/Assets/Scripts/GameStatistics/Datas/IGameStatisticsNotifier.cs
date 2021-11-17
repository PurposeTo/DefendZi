using System;

public interface IGameStatisticsNotifier
{
    public event Action OnTotalInAppTimeChanged;
    public event Action OnTotalLifeTimeChanged;
    public event Action OnAverageLifeTimeChanged;
    public event Action OnGamesNumberChanged;
    public event Action OnBestScoreChanged;
    public event Action OnBestLifeTimeChanged;
}
