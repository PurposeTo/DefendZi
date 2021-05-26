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

        public override void Initialize()
        {
            T value = initFunc.Invoke();
            Set(value);
        }

        public override T Get()
        {
            Initialize();
            //Получаем состояние
            return state.Get()
            //Получаем значение
                .Get();
        }

        public override void Set(T value)
        {
            //Изменяем состояние
            state.SetAndGet(new InitedValue<T>(state, initFunc, valueRef))
            //Изменяем значение
                .Set(value);
        }

        public override T SetAndGet(T value)
        {
            //Изменяем состояние
            return state.SetAndGet(new InitedValue<T>(state, initFunc, valueRef))
            //Изменяем значение
                .SetAndGet(value);
        }
    }
}
