using System;

namespace Desdiene.Types.PercentAsset
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
