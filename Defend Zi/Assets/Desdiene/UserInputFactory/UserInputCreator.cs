using System;
using System.Linq;
using Desdiene.Extensions.System;
using UnityEngine;

namespace Desdiene.UserInputFactory
{
    public class UserInputCreator<T> : IUserInputCreator<T>
    {
        private readonly UserInputViaPlatform<T>[] controllers;

        public UserInputCreator(UserInputViaPlatform<T>[] controllers)
        {
            this.controllers = controllers;
        }

        public T GetOrDefault() => GetOrDefault(Application.platform);

        public T GetOrDefault(RuntimePlatform platform)
        {
            UserInputViaPlatform<T> controllerViaPlatform = controllers
                .Where(controller => controller.Platform == platform)
                .FirstOrDefault(() =>
                {
                    Debug.LogError($"{platform} is unknown platform!");
                    return GetDefault();
                });
            Debug.Log($"Create controller with {controllerViaPlatform.Platform}!");
            //TODO: возвращаемая сущность по хорошему должна быть синглтоном.
            return controllerViaPlatform.CreateUserInput();
        }

        private UserInputViaPlatform<T> GetDefault()
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
