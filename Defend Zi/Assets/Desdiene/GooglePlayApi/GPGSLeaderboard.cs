using System;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using Zenject;

namespace Desdiene.GooglePlayApi
{
    public class GpgsLeaderboard : MonoBehaviourExt
    {
        private PlayGamesPlatform _platform;
        private IGameStatisticsAccessor _gameStatistics;

        [Inject]
        private void Constructor(GpgsAutentification platformCreator, GameStatistics gameStatistics)
        {
            if (platformCreator == null) throw new ArgumentNullException(nameof(platformCreator));

            _platform = platformCreator.Get();
            _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
        }

        public void Open()
        {
            AddBestScore(_gameStatistics.BestScore, OpenLeaderboard);
        }

        private void OpenLeaderboard()
        {
            _platform.ShowLeaderboardUI();
        }

        private void AddBestScore(uint bestScore, Action onAdded)
        {
            _platform.ReportScore(bestScore, GPGSIds.leaderboard_the_most_careful, (success) =>
            {
                if (success)
                {
                    onAdded?.Invoke();
                }
            });
        }
    }
}
