using System;
using System.Collections.Generic;
using UnityEngine;

namespace Desdiene.UserInputFactory
{
    public class UserInputCreator<T> : IUserInputCreator<T>
    {
        private readonly IDictionary<RuntimePlatform, Func<T>> userInputs;

        public UserInputCreator(IDictionary<RuntimePlatform, Func<T>> userInputs)
        {
            this.userInputs = userInputs;
        }

        public T GetOrDefault() => GetOrDefault(Application.platform);

        public T GetOrDefault(RuntimePlatform platform)
        {
            if (userInputs.TryGetValue(platform, out Func<T> userInputGetter))
            {
                Debug.Log($"Creating userInput with {platform}");
                return userInputGetter.Invoke();
            }
            else
            {
                Debug.LogError($"{platform} is unknown platform");
                return GetDefault();
            }
        }

        private T GetDefault()
        {
            RuntimePlatform defaultPlatform = RuntimePlatform.WindowsEditor;
            if (userInputs.TryGetValue(defaultPlatform, out Func<T> userInputGetter))
            {
                Debug.LogError($"Creating userInput with default platfrom: {defaultPlatform}");
                return userInputGetter.Invoke();
            }
            else throw new NullReferenceException($"Не установленно значение для платформы по умолчанию: {defaultPlatform}");
        }
    }
}
