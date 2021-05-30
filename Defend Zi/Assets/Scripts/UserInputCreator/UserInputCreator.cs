using Desdiene.UserInputFactory;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class UserInputCreator : IUserInputCreator<IUserInput>
{
    private readonly UserInputCreator<IUserInput> creator;

    public UserInputCreator(MonoBehaviourExt mono)
    {
        UserInputViaPlatform<IUserInput>[] controllers =
            {
            new UserInputViaPlatform<IUserInput>(RuntimePlatform.Android, () => new MobileInput(mono)),
            new UserInputViaPlatform<IUserInput>(RuntimePlatform.WindowsEditor, () => new EditorInput(mono))
        };

        creator = new UserInputCreator<IUserInput>(controllers);
    }

    public IUserInput GetOrDefault() => creator.GetOrDefault();

    public IUserInput GetOrDefault(RuntimePlatform platform) => creator.GetOrDefault(platform);
}
