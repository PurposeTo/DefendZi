using System;
using UnityEngine;

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
            OnValueGetting += Initialize;
        }

        /// <summary>
        /// Проинициализировать поле, если оно null.
        /// </summary>
        public void Initialize()
        {
            if(IsNull())
            {
                Set(initization.Invoke());
                if (IsNull()) Debug.LogError("Value wasn't initialize by initializer!");
            }
        }
    }
}
