using System;

namespace Desdiene.Types.AtomicReferences.Init.States.Base
{
    internal abstract class State<T>
    {
        private readonly IRef<State<T>> _state;
        protected readonly Func<T> initFunc;
        protected readonly IRef<T> valueRef;

        protected State(in IRef<State<T>> state, in Func<T> initFunc, in IRef<T> valueRef)
        {
            this._state = state ?? throw new ArgumentNullException(nameof(state));
            this.initFunc = initFunc ?? throw new ArgumentNullException(nameof(initFunc));
            this.valueRef = valueRef ?? throw new ArgumentNullException(nameof(valueRef));
        }

        public abstract T GetOrInit();
        public abstract void Set(T value);
        public abstract T SetAndGet(T value);

        protected State<T> SetState(Func<IRef<State<T>>, State<T>> newState)
        {
            return _state.SetAndGet(newState.Invoke(_state));
        }
    }
}
