using System;
using Desdiene.Types.AtomicReference.Api;
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
            add { valueRef.OnValueChanged += value; }
            remove { valueRef.OnValueChanged -= value; }
        }

        public void Initialize(Func<T> initFunc)
        {
            if (initStateRef.Get() is NotInitedValue<T>)
            {
                initStateRef.Set(new NotInitedValue<T>(initStateRef, initFunc, valueRef));
            }
            //Не кешировать, так как ссылка могла поменяться
            initStateRef.Get().Initialize();
        }

        public T Get(Func<T> initFunc)
        {
            if (initStateRef.Get() is NotInitedValue<T>)
            {
                initStateRef.Set(new NotInitedValue<T>(initStateRef, initFunc, valueRef));
            }
            //Не кешировать, так как ссылка могла поменяться
            //Получаем состояние
            return initStateRef.Get()
            //Получаем значение
                .Get();
        }

        public void Set(T value)
        {
            //Получаем состояние
            initStateRef.Get()
            //Изменяем значение
                .Set(value);
        }

        public T SetAndGet(T value)
        {
            //Получаем состояние
            return initStateRef.Get()
            //Изменяем значение
                .SetAndGet(value);
        }
    }
}
