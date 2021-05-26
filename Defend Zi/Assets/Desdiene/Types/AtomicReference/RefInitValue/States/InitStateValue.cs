using System;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit.States
{
    internal abstract class InitStateValue<T>
    {
        protected readonly Ref<InitStateValue<T>> state;
        protected readonly Func<T> initFunc;
        protected readonly Ref<T> valueRef;

        protected InitStateValue(in Ref<InitStateValue<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.initFunc = initFunc ?? throw new ArgumentNullException(nameof(initFunc));
            this.valueRef = valueRef ?? throw new ArgumentNullException(nameof(valueRef));
        }

        public abstract void Initialize();
        public abstract T Get();
        public abstract void Set(T value);
        public abstract T SetAndGet(T value);
    }
}
