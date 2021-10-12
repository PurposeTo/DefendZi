using System;

namespace Desdiene.Types.Percents
{
    public interface IPercentNotifier
    {
        event Action OnChanged;
    }
}
