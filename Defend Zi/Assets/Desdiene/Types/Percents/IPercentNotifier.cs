using System;

namespace Desdiene.Types.Percents
{
    public interface IPercentNotifier
    {
        event Action OnValueChanged;
    }

    public interface IPercentOnChanged<T>
    {
        event Action<T> OnValueChanged;
    }
}
