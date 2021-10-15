using System;
using Desdiene.DataStorageFactories.DataContainers;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using Zenject;

namespace Desdiene.GooglePlayApi
{
    public class GpgsLeaderboard : MonoBehaviourExt
    {
        private PlayGamesPlatform _platform;
        private IDataContainer<IGameData> _storage;

        [Inject]
        private void Constructor(GpgsAutentification platformCreator, IDataContainer<IGameData> storage)
        {
            if (platformCreator == null) throw new ArgumentNullException(nameof(platformCreator));

            _platform = platformCreator.Get();
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        private IGameData GameData => _storage.GetData();

        public void Open()
        {
            AddBestScore(GameData.BestScore, OpenLeaderboard);
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
