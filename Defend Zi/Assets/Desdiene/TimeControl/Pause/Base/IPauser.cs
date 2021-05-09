namespace Desdiene.TimeControl.Pause.Base
{
    public interface IPauser
    {
        bool IsPause { get; }
        void Add(PausableTime pausable);
        void Remove(PausableTime pausable);
    }
}
