using System;
using Desdiene.Types.AtomicReference.Interfaces;
using Desdiene.Types.AtomicReference.RefRuntimeInit.States;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class RefRuntimeInit<T> : IWriteRef<T>
    {
        private readonly Ref<InitStateValue<T>> initStateRef = new Ref<InitStateValue<T>>();
        private InitStateValue<T> InitState => initStateRef.Get();

        private readonly Ref<T> valueRef = new Ref<T>();

        public RefRuntimeInit()
        {
            initStateRef.Set(new NotInitedValue<T>(
                initStateRef,
                () => throw new NullReferenceException("Метод инициализации значения не задан."),
                valueRef));
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnValueChanged += value;
            remove => valueRef.OnValueChanged -= value;
        }

        /// <summary>
        /// Если поле не было проинициализированно, сделать это. 
        /// Получить значение поля.
        /// </summary>
        /// <param name="initFunc"></param>
        /// <returns></returns>
        public T GetOrInit(Func<T> initFunc)
        {
            //установить новое значение initFunc
            if (InitState is NotInitedValue<T>)
            {
                initStateRef.Set(new NotInitedValue<T>(initStateRef, initFunc, valueRef));
            }
            return InitState.GetOrInit();
        }

        public void Set(T value) => InitState.Set(value);

        public T SetAndGet(T value) => InitState.SetAndGet(value);
    }
}
