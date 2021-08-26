using Desdiene.TimeControls.Pausers;

namespace Desdiene.TimeControls.Pausables
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
