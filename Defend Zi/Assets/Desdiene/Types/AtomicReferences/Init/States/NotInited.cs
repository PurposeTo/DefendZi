using System;
using Desdiene.Types.AtomicReferences.Init.States.Base;

namespace Desdiene.Types.AtomicReferences.Init.States
{
    internal class NotInited<T> : State<T>
    {
        public NotInited(in IRef<State<T>> state, in Func<T> initFunc, in IRef<T> valueRef)
            : base(state, initFunc, valueRef)
        {
            //Проверка на null нужна только если мы собираемся использовать данное значение.
            if (initFunc is null) throw new ArgumentNullException(nameof(initFunc));
        }

        public override T GetOrInit()
        {
            T value = initFunc.Invoke();
            return SetAndGet(value);
        }

        public override void Set(T value)
        {
            SetState((currState) => new Inited<T>(currState, initFunc, valueRef))
                .Set(value);
        }

        public override T SetAndGet(T value)
        {
            return SetState((currState) => new Inited<T>(currState, initFunc, valueRef))
                .SetAndGet(value);
        }
    }
}
