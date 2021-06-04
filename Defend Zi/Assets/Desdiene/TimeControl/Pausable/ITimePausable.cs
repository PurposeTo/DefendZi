using Desdiene.TimeControl.Pauser;

namespace Desdiene.TimeControl.Pausable
{
    /// <summary>
    /// Описывает время, которое можно поставить на паузу.
    /// Пауза вычисляется на основании данных в ITimePauser-ах, которые можно "подключить".
    /// </summary>
    public interface ITimePausable
    {
        bool IsPause { get; }
        void Add(ITimePauser pauser);
        void Remove(ITimePauser pauser);
    }
}
