using System;
namespace Desdiene.TimeControl.Pause
{
    public interface IPausableTime
    {
        string Name { get; }
        event Action OnPauseChanged;
        bool IsPause { get; }
        void SetPause(bool isPause);
    }
}
