using System;

namespace Desdiene.Types.AtomicReferences
{
    /// <summary>
    /// Ссылочная обертка для значения. 
    /// Имеет событие об изменении значения.
    /// Если не указывать значение в конструкторе, будет иметь значение по умолчанию.
    /// Класс запечатан. Для использовании логики использовать композицию.
    /// </summary>
    /// <typeparam name="T">Тип значения.</typeparam>
    [Serializable]
    public sealed class Ref<T> : IRef<T>
    {
        private T _value;

        public Ref() : this(default) { }
        public Ref(T value)
        {
            Set(value);
        }

        private event Action OnChanged;

        T IRefAccessor<T>.Value => _value;

        event Action IRefNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        void IRefMutator<T>.Set(T value) => Set(value);

        T IRef<T>.SetAndGet(T value)
        {
            Set(value);
            return _value;
        }

        private void Set(T value)
        {
            if (!Equals(_value, value))
            {
                this._value = value;
                OnChanged?.Invoke();
            }
        }
    }
}
