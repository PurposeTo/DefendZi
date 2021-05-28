using System;

namespace Desdiene.Types.AtomicReference.Interfaces
{
    public interface IRefOnChanged
    {
        event Action OnValueChanged;
    }

    public interface IRefOnChanged<T>
    {
        event Action<T> OnValueChanged;
    }
}
