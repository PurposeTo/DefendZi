using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UserInputFactories;
using UnityEngine;

public class UserInputFactory : IUserInputFactory<IUserInput>
{
    private readonly UserInputFactory<IUserInput> creator;

    public UserInputFactory(MonoBehaviourExt mono)
    {
        IDictionary<RuntimePlatform, Func<IUserInput>> userInputs = new Dictionary<RuntimePlatform, Func<IUserInput>>
        {
            { RuntimePlatform.Android, () => new MobileInput(mono) },
            { RuntimePlatform.WindowsEditor, () => new EditorInput(mono) }
        };

        creator = new UserInputFactory<IUserInput>(userInputs);
    }

    public IUserInput GetOrDefault() => creator.GetOrDefault();

    public IUserInput GetOrDefault(RuntimePlatform platform) => creator.GetOrDefault(platform);
}
