using System;

namespace Desdiene.Types.EventContainers
{
    /// <summary>
    /// Обертка для event, чтобы с ним можно было работать как с объектом.
    /// </summary>
    public interface IInitionEvent<T>
    {
        event Action<T> OnInited;
    }
}
