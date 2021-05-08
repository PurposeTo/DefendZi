namespace Desdiene.AtomicReference
{
    public class AtomicRef<T>
    {
        private protected T value;

        public AtomicRef() { }
        public AtomicRef(T value)
        {
            Set(value);
        }

        public void Set(T value) => this.value = value;

        public virtual T Get() => value;

        public bool IsNull()
        {
            return value == null;
        }
    }
}
