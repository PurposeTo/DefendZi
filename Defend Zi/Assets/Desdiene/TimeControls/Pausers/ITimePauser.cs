using System;
namespace Desdiene.TimeControls.Pausers
{
    /// <summary>
    /// Описывает объект, который может поставить время на паузу. 
    /// Таких объектов может быть несколько. Если хотя бы один на паузе, то время на паузе.
    /// </summary>
    public interface ITimePauser
    {
        string Name { get; }
        event Action OnPauseChanged;
        bool IsPause { get; }
        void SetPause(bool isPause);
    }
}
