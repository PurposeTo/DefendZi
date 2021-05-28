using System;

namespace Desdiene.Types.AtomicReference.Interfaces
{
    /// <summary>
    /// Интерфейс для чтения значения и получении уведомления о его изменении.
    /// </summary>
    /// <typeparam name="T">Тип значения.</typeparam>
    public interface IReadRef<T>
    {
        event Action OnValueChanged;
        T Get();
    }
}
