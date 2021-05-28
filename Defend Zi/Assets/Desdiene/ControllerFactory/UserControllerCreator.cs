using System;
using System.Linq;
using Desdiene.Extensions.System;
using UnityEngine;

namespace Desdiene.ControllerFactory
{
    public class UserControllerCreator<T> : IUserControllerCreator<T>
    {
        private readonly UserControllerViaPlatform<T>[] controllers;

        public UserControllerCreator(UserControllerViaPlatform<T>[] controllers)
        {
            this.controllers = controllers;
        }

        public T GetOrDefault() => GetOrDefault(Application.platform);

        public T GetOrDefault(RuntimePlatform platform)
        {
            UserControllerViaPlatform<T> controllerViaPlatform = controllers
                .Where(controller => controller.Platform == platform)
                .FirstOrDefault(() =>
                {
                    Debug.LogError($"{platform} is unknown platform!");
                    return GetDefault();
                });
            Debug.Log($"Create controller with {controllerViaPlatform.Platform}!");
            return controllerViaPlatform.CreateController();
        }

        private UserControllerViaPlatform<T> GetDefault()
        {
            RuntimePlatform defaultPlatform = RuntimePlatform.WindowsEditor;
            return controllers
                .Where(controller => controller.Platform == defaultPlatform)
                .FirstOrDefault(() =>
                {
                    throw new NullReferenceException($"Не установленно значение для платформы по умолчанию: {defaultPlatform}");
                });
        }
    }
}
