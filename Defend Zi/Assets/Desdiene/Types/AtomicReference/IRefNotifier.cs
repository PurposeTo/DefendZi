using System;

namespace Desdiene.Types.AtomicReference
{
    public interface IRefNotifier
    {
        event Action OnValueChanged;
    }

    public interface IRefNotifier<T>
    {
        event Action<T> OnValueChanged;
    }
}
