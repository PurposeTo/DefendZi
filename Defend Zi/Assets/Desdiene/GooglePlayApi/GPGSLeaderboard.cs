using System;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using Zenject;

namespace Desdiene.GooglePlayApi
{
    public class GPGSLeaderboard : MonoBehaviourExt
    {
        private IGPGSAuthentication _authentication;
        private IStorage<IGameData> _storage;

        [Inject]
        private void Constructor(IGPGSAuthentication authentication, IStorage<IGameData> storage)
        {
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        private IGameData GameData => _storage.GetData();
        private PlayGamesPlatform Platform => _authentication.Platform;

        public void Open()
        {
            AddBestScore(GameData.BestScore, OpenLeaderboard);
        }

        private void OpenLeaderboard()
        {
            Platform?.ShowLeaderboardUI();
        }

        private void AddBestScore(int bestScore, Action onAdded)
        {
            Platform?.ReportScore(bestScore, GPGSIds.leaderboard_the_most_careful, (success) =>
            {
                if (success)
                {
                    onAdded?.Invoke();
                }
            });
        }
    }
}
