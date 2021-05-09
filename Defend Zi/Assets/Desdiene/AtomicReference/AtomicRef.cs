using System;

namespace Desdiene.AtomicReference
{
    public class AtomicRef<T>
    {
        public T value;
        public event Action OnValueChanged;

        public AtomicRef() { }
        public AtomicRef(T value)
        {
            Set(value);
        }

        public void Set(T value)
        {
            if(Equals(this.value, value))
            {
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }

        //Используется именно метод вместо свойства потому, что он virtual. С методом проще работать.
        public virtual T Get() => value;

        public bool IsNull()
        {
            return value == null;
        }
    }
}
