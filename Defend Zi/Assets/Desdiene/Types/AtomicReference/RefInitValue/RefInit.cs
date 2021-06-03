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
    public class RefInit<T> : IRef<T>
    {
        private readonly Ref<InitStateValue<T>> initStateRef = new Ref<InitStateValue<T>>();
        private InitStateValue<T> InitState => initStateRef.Get();
        private readonly Ref<T> valueRef = new Ref<T>();

        /// <param name="initization">Метод для инициализации поля</param>
        public RefInit(Func<T> initFunc)
        {
            initStateRef.Set(new NotInitedValue<T>(initStateRef, initFunc, valueRef));
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnValueChanged += value;
            remove => valueRef.OnValueChanged -= value;
        }

        T IReadRef<T>.Get() => GetOrInit(); // Lazy get
        public T GetOrInit() => InitState.GetOrInit();

        public void Set(T value) => InitState.Set(value);

        public T SetAndGet(T value) => InitState.SetAndGet(value);

        void IWriteRef<T>.Set(T value)
        {
            throw new NotImplementedException();
        }
    }
}
