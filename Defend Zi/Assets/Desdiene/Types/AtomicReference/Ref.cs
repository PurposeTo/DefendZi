using System;
using Desdiene.Types.AtomicReference.Interfaces;

namespace Desdiene.Types.AtomicReference
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
        public Ref() : this(default) { }
        public Ref(T value)
        {
            Set(value);
        }

        public event Action OnValueChanged;
        private T value;

        public T Get() => value;

        public void Set(T value)
        {
            if (!Equals(this.value, value))
            {
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }

        public T SetAndGet(T value)
        {
            Set(value);
            return Get();
        }

        //todo входит ли данный метод в логику данного класса?
        public bool IsNull() => value == null;
    }
}
