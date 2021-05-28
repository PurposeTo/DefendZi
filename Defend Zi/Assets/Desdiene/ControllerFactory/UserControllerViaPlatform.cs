using System;
using UnityEngine;

namespace Desdiene.ControllerFactory
{
    /// <summary>
    /// Связывает enam RuntimePlatform и метод создания контроллера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserControllerViaPlatform<T>
    {
        public RuntimePlatform Platform { get; }
        private readonly Func<T> getController;

        public UserControllerViaPlatform(RuntimePlatform platform, Func<T> getController)
        {
            Platform = platform;
            this.getController = getController ?? throw new ArgumentNullException(nameof(platform));
        }

        public T CreateController()
        {
            return getController.Invoke() ?? throw new NullReferenceException(nameof(getController));
        }
    }
}