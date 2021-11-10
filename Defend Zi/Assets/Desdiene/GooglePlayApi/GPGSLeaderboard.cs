using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Desdiene.GooglePlayApi
{
    public class GpgsLeaderboard
    {
        private readonly string _leaderboardId;
        private readonly PlayGamesPlatform _platform;
        private long _cashScore = -1; // По умолчанию -1, если счет не был успешно обновлен в таблице лидеров
        private bool _isCashScoreInited = false;

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
            _platform.ShowLeaderboardUI(_leaderboardId, (status) =>
            {
                Debug.Log($"{GetType().Name} opened with status {status}");
            });
        }

        public void UpdateScore(long score, Action<bool> result)
        {
            if (_isCashScoreInited && _cashScore == score)
            {
                result?.Invoke(true);
                return;
            }

            _platform.ReportScore(score, _leaderboardId, (success) =>
            {
                if (success)
                {
                    _isCashScoreInited = true;
                    _cashScore = score;
                }

                result?.Invoke(success);
            });
        }
    }
}
