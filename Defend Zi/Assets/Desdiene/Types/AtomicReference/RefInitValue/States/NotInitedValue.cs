using System;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit.States
{
    internal class NotInitedValue<T> : InitStateValue<T>
    {
        public NotInitedValue(in Ref<InitStateValue<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
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
            SetState((currState) => new InitedValue<T>(currState, initFunc, valueRef))
                .Set(value);
        }

        public override T SetAndGet(T value)
        {
            return SetState((currState) => new InitedValue<T>(currState, initFunc, valueRef))
                .SetAndGet(value);
        }
    }
}
