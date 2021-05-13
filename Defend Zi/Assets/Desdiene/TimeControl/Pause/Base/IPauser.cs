namespace Desdiene.TimeControl.Pause.Base
{
    public interface IPauser
    {
        bool IsPause { get; }
        void Add(IPausableTime pausable);
        void Remove(IPausableTime pausable);
    }
}
