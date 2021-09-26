using System;

namespace Desdiene.Types.AtomicReferences
{
    public interface IRefNotifier
    {
        event Action OnChanged;
    }

    public interface IRefNotifier<T>
    {
        event Action<T> OnValueChanged;
    }
}
