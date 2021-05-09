using System;

namespace Desdiene.AtomicReference
{
    public class AtomicRef<T>
    {
        public T value;
        public event Action OnValueChanged;
        //Перед получением значения можно выполнить дополнительные вычисления в дочернем классе (Шаблонный метод через событие).
        protected event Action OnValueGetting; 

        public AtomicRef() { }
        public AtomicRef(T value)
        {
            Set(value);
        }

        public void Set(T value)
        {
            if(!Equals(this.value, value))
            {
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }

        public T Get()
        {
            OnValueGetting?.Invoke();
            return value;
        }

        public bool IsNull()
        {
            return value == null;
        }
    }
}
