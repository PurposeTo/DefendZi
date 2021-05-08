using System;

namespace Desdiene.AtomicReference
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class AtomicRefRuntimeInit<T>
    {
        private readonly AtomicRef<T> value = new AtomicRef<T>();

        private Func<T> initization;

        /// <summary>
        /// Проинициализировать поле, если оно null, с помощью передаваемого метода.
        /// </summary>
        /// <param name="initization">Метод для инициализации поля.</param>
        public void Initialize(Func<T> initization)
        {
            if (this.initization == null)
            {
                this.initization = initization ?? throw new ArgumentNullException(nameof(initization));
            }

            if (IsNull())
            {
                value.Set(this.initization.Invoke());
            }
        }

        /// <summary>
        /// Получить значение поля. 
        /// Если оно не проинициализированно, проинициализировать с помощью передаваемого метода.
        /// </summary>
        /// <param name="initization">Метод для инициализации поля.</param>
        /// <returns>Поле.</returns>
        public T Get(Func<T> initization)
        {
            Initialize(initization);
            return value.Get();
        }


        public bool IsNull() => value.IsNull();
    }
}
