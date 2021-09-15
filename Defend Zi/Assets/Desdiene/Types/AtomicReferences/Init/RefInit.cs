using System;
using Desdiene.Types.AtomicReferences.Init.States;
using Desdiene.Types.AtomicReferences.Init.States.Base;

namespace Desdiene.Types.AtomicReferences.Init
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class RefInit<T> : IRef<T>
    {
        private readonly Ref<State<T>> initStateRef = new Ref<State<T>>();
        private State<T> InitState => initStateRef.Get();
        private readonly Ref<T> valueRef = new Ref<T>();

        /// <param name="initization">Метод для инициализации поля</param>
        public RefInit(Func<T> initFunc)
        {
            initStateRef.Set(new NotInited<T>(initStateRef, initFunc, valueRef));
        }

        public event Action OnValueChanged
        {
            add => valueRef.OnValueChanged += value;
            remove => valueRef.OnValueChanged -= value;
        }

        T IRefAccessor<T>.Get() => GetOrInit(); // Lazy get
        public T GetOrInit() => InitState.GetOrInit();

        public void Set(T value) => InitState.Set(value);

        public T SetAndGet(T value) => InitState.SetAndGet(value);

        void IRefMutator<T>.Set(T value)
        {
            throw new NotImplementedException();
        }
    }
}
