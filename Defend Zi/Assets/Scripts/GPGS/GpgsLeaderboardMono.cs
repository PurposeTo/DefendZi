using System;
using Desdiene.Mobile.GooglePlayApi;
using Desdiene.MonoBehaviourExtension;
using Desdiene.NetworkServices;
using Zenject;

public class GpgsLeaderboardMono : MonoBehaviourExt
{
    private ILeaderboard _gpgsLeaderboard;
    private IGameStatisticsAccessorNotifier _gameStatistics;

    [Inject]
    private void Constructor(GpgsAutentification platformCreator, IGameStatistics gameStatistics)
    {
        if (platformCreator == null) throw new ArgumentNullException(nameof(platformCreator));

        var platform = platformCreator.Get();
        var leaderboardId = GPGSIds.leaderboard_the_most_careful;
        _gpgsLeaderboard = new GpgsLeaderboard(platform, leaderboardId);

        _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
    }

    public void UpdateScoreAndOpen()
    {
        _gpgsLeaderboard.UpdateAndOpen(_gameStatistics.BestScore, (_) => { });
    }
}
