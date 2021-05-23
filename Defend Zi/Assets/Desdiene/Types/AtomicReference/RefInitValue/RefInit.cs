using System;
using Desdiene.Types.AtomicReference.RefRuntimeInit.States;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class RefInit<T> : IRef<T>
    {
        private readonly Ref<InitStateValue<T>> initState = new Ref<InitStateValue<T>>();
        private readonly Ref<T> valueRef = new Ref<T>();

        /// <param name="initization">Метод для инициализации поля</param>
        public RefInit(Func<T> initFunc)
        {
            initState.Set(new NotInitializedValue<T>(initState, initFunc, valueRef));
        }

        public event Action OnValueChanged
        {
            add { valueRef.OnValueChanged += value; }
            remove { valueRef.OnValueChanged -= value; }
        }

        public void Initialize() => initState.Get().Initialize();

        public T Get()
        {
            //Получаем состояние
            return initState.Get()
            //Получаем значение
                .Get();
        }

        public void Set(T value)
        {
            //Получаем состояние
            initState.Get()
            //Изменяем значение
                .Set(value);
        }

        public T SetAndGet(T value)
        {
            //Получаем состояние
            return initState.Get()
            //Изменяем значение
                .SetAndGet(value);
        }
    }
}
