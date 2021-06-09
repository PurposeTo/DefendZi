using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.UserInputFactory;
using UnityEngine;

public class UserInputCreator : IUserInputCreator<IUserInput>
{
    private readonly UserInputCreator<IUserInput> creator;

    public UserInputCreator(MonoBehaviourExt mono)
    {
        IDictionary<RuntimePlatform, Func<IUserInput>> userInputs = new Dictionary<RuntimePlatform, Func<IUserInput>>
        {
            { RuntimePlatform.Android, () => new MobileInput(mono) },
            { RuntimePlatform.WindowsEditor, () => new EditorInput(mono) }
        };

        creator = new UserInputCreator<IUserInput>(userInputs);
    }

    public IUserInput GetOrDefault() => creator.GetOrDefault();

    public IUserInput GetOrDefault(RuntimePlatform platform) => creator.GetOrDefault(platform);
}
