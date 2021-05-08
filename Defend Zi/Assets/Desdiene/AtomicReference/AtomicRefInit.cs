using System;

namespace Desdiene.AtomicReference
{
    /// <summary>
    /// Класс, хранящий одно поле.
    /// Инициализация данного поля произойдет либо при его получении, либо при вызове соответстующего метода.
    /// </summary>
    /// <typeparam name="T">Тип поля в данном классе.</typeparam>
    public class AtomicRefInit<T> : AtomicRef<T>
    {
        private readonly Func<T> initization;

        /// <param name="initization">Метод для инициализации поля</param>
        public AtomicRefInit(Func<T> initization)
        {
            this.initization = initization ?? throw new ArgumentNullException(nameof(initization));
        }

        /// <summary>
        /// Проинициализировать поле, если оно null.
        /// </summary>
        public void Initialize()
        {
            if(IsNull()) Set(initization.Invoke());
        }

        /// <summary>
        /// Получить проинициализированное поле.
        /// </summary>
        /// <returns>Поле.</returns>
        public override T Get()
        {
            Initialize();
            return value;
        }
    }
}
