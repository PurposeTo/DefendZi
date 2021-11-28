using System;
using Desdiene.NetworkServices;
using GooglePlayGames;
using UnityEngine;

namespace Desdiene.Mobile.GooglePlayApi
{
    public class GpgsLeaderboard : ILeaderboard
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

        void ILeaderboard.Open(Action<bool> result) => Open(result);

        void ILeaderboard.Update(long score, Action<bool> result) => Update(score, result);

        void ILeaderboard.UpdateAndOpen(long score, Action<bool> result) => UpdateAndOpen(score, result);

        private void UpdateAndOpen(long score, Action<bool> result)
        {
            Update(score, (sucessUpdated) =>
            {
                if (!sucessUpdated)
                {
                    result?.Invoke(false);
                    return;
                }

                Open(result);
            });
        }

        private void Update(long score, Action<bool> result)
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

        private void Open(Action<bool> result)
        {
            GooglePlayGames.BasicApi.UIStatus successStatus = GooglePlayGames.BasicApi.UIStatus.Valid;

            _platform.ShowLeaderboardUI(_leaderboardId, (status) =>
            {
                Debug.Log($"{GetType().Name} opened with status {status}");

                bool success = successStatus == status;
                result?.Invoke(success);
            });
        }
    }
}
