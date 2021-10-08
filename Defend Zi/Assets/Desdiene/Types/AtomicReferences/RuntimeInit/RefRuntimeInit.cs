using System;
using Desdiene.Types.AtomicReferences.Init.States;
using Desdiene.Types.AtomicReferences.Init.States.Base;

namespace Desdiene.Types.AtomicReferences.RuntimeInit
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class RefRuntimeInit<T> : IRefMutator<T>
    {
        private readonly IRef<State<T>> initStateRef = new Ref<State<T>>();
        private readonly IRef<T> valueRef = new Ref<T>();

        public RefRuntimeInit()
        {
            initStateRef.Set(new NotInited<T>(initStateRef,
                                              () => throw new NullReferenceException("Метод инициализации значения не задан."),
                                              valueRef));
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnChanged += value;
            remove => valueRef.OnChanged -= value;
        }

        private State<T> InitState => initStateRef.Value;

        /// <summary>
        /// Если поле не было проинициализированно, сделать это. 
        /// Получить значение поля.
        /// </summary>
        /// <param name="initFunc"></param>
        /// <returns></returns>
        public T GetOrInit(Func<T> initFunc)
        {
            //установить новое значение initFunc
            if (InitState is NotInited<T>)
            {
                initStateRef.Set(new NotInited<T>(initStateRef, initFunc, valueRef));
            }
            return InitState.GetOrInit();
        }

        public void Set(T value) => InitState.Set(value);

        public T SetAndGet(T value) => InitState.SetAndGet(value);
    }
}
