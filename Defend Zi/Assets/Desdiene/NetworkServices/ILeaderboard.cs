using System;

namespace Desdiene.NetworkServices
{
    public interface ILeaderboard
    {
        void UpdateAndOpen(long score, Action<bool> result);
        void Update(long score, Action<bool> result);
        void Open(Action<bool> result);
    }
}