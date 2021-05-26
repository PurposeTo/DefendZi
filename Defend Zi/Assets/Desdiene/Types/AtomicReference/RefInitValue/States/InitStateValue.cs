using System;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit.States
{
    internal abstract class InitStateValue<T>
    {
        protected readonly Func<T> initFunc;
        protected readonly Ref<T> valueRef;
        private readonly Ref<InitStateValue<T>> state;

        protected InitStateValue(in Ref<InitStateValue<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.initFunc = initFunc ?? throw new ArgumentNullException(nameof(initFunc));
            this.valueRef = valueRef ?? throw new ArgumentNullException(nameof(valueRef));
        }

        public abstract T GetOrInit();
        public abstract void Set(T value);
        public abstract T SetAndGet(T value);

        protected InitStateValue<T> SetState(Func<Ref<InitStateValue<T>>, InitStateValue<T>> newState)
        {
            return state.SetAndGet(newState.Invoke(state));
        }
    }
}
