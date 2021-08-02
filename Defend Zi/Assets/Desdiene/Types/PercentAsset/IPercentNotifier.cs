using System;

namespace Desdiene.Types.Percent
{
    public interface IPercentOnChanged
    {
        event Action OnValueChanged;
    }

    public interface IPercentOnChanged<T>
    {
        event Action<T> OnValueChanged;
    }
}
