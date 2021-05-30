using Desdiene.ControllerFactory;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class UserControllerCreator : IUserControllerCreator<IUserInput>
{
    private readonly UserControllerCreator<IUserInput> creator;

    public UserControllerCreator(MonoBehaviourExt mono)
    {
        UserControllerViaPlatform<IUserInput>[] controllers =
            {
            new UserControllerViaPlatform<IUserInput>(RuntimePlatform.Android, () => new MobileController(mono)),
            new UserControllerViaPlatform<IUserInput>(RuntimePlatform.WindowsEditor, () => new EditorController(mono))
        };

        creator = new UserControllerCreator<IUserInput>(controllers);
    }

    public IUserInput GetOrDefault() => creator.GetOrDefault();

    public IUserInput GetOrDefault(RuntimePlatform platform) => creator.GetOrDefault(platform);
}
