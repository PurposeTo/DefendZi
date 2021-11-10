using System;
using Desdiene.MonoBehaviourExtension;
using Zenject;

namespace Desdiene.GooglePlayApi
{
    public class GpgsLeaderboardMono : MonoBehaviourExt
    {
        private GpgsLeaderboard _gpgsLeaderboard;
        private IGameStatisticsAccessorNotifier _gameStatistics;

        [Inject]
        private void Constructor(GpgsAutentification platformCreator, GameStatistics gameStatistics)
        {
            if (platformCreator == null) throw new ArgumentNullException(nameof(platformCreator));

            var platform = platformCreator.Get();
            var leaderboardId = GPGSIds.leaderboard_the_most_careful;
            _gpgsLeaderboard = new GpgsLeaderboard(platform, leaderboardId);

            _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
        }

        public void UpdateScoreAndOpen()
        {
            _gpgsLeaderboard.UpdateScore(_gameStatistics.BestScore, (success) =>
            {
                if (success) _gpgsLeaderboard.Open();
            });
        }
    }
}
