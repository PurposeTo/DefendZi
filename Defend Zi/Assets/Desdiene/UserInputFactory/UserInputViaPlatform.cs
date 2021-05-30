using System;
using UnityEngine;

namespace Desdiene.UserInputFactory
{
    /// <summary>
    /// Связывает enam RuntimePlatform и метод создания контроллера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserInputViaPlatform<T>
    {
        public RuntimePlatform Platform { get; }
        private readonly Func<T> getController;

        public UserInputViaPlatform(RuntimePlatform platform, Func<T> getController)
        {
            Platform = platform;
            this.getController = getController ?? throw new ArgumentNullException(nameof(platform));
        }

        public T CreateUserInput()
        {
            return getController.Invoke() ?? throw new NullReferenceException(nameof(getController));
        }
    }
}