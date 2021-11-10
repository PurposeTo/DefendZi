using System;
using GooglePlayGames;

namespace Desdiene.GooglePlayApi
{
    public class GpgsLeaderboard
    {
        private readonly string _leaderboardId;
        private readonly PlayGamesPlatform _platform;
        private long _cashScore = -1; // По умолчанию -1, если счет не был успешно обновлен в таблице лидеров

        public GpgsLeaderboard(PlayGamesPlatform platform, string leaderboardId)
        {
            if (string.IsNullOrWhiteSpace(leaderboardId))
            {
                throw new ArgumentException($"\"{nameof(leaderboardId)}\" can't be null or white space");
            }

            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _leaderboardId = leaderboardId;
        }

        public void Open()
        {
            _platform.ShowLeaderboardUI(_leaderboardId);
        }

        public void UpdateScore(long score, Action<bool> result)
        {
            _platform.ReportScore(score, _leaderboardId, (success) =>
            {
                if (success) _cashScore = score;
                result?.Invoke(success);
            });
        }
    }
}
