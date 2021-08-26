using System;

namespace Desdiene.Types.AtomicReferences.Init.States.Base
{
    internal abstract class State<T>
    {
        protected readonly Func<T> initFunc;
        protected readonly Ref<T> valueRef;
        private readonly Ref<State<T>> state;

        protected State(in Ref<State<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            this.initFunc = initFunc ?? throw new ArgumentNullException(nameof(initFunc));
            this.valueRef = valueRef ?? throw new ArgumentNullException(nameof(valueRef));
        }

        public abstract T GetOrInit();
        public abstract void Set(T value);
        public abstract T SetAndGet(T value);

        protected State<T> SetState(Func<Ref<State<T>>, State<T>> newState)
        {
            return state.SetAndGet(newState.Invoke(state));
        }
    }
}
