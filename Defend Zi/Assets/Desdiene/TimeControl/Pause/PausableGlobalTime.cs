using Desdiene.TimeControl.Pause.Base;

namespace Desdiene.TimeControl.Pause
{
    /// <summary>
    /// Создаваемый объект дает возможность поставить на паузу глобальное время.
    /// </summary>
    public class PausableGlobalTime : PausableTime
    {
        public PausableGlobalTime(string name) : base(GlobalPauser.Instance, name) { }
    }
}
