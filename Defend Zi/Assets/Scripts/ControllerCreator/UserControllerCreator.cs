using Desdiene.ControllerFactory;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class UserControllerCreator : IUserControllerCreator<IUserController>
{
    private readonly UserControllerCreator<IUserController> creator;

    public UserControllerCreator(MonoBehaviourExt mono)
    {
        UserControllerViaPlatform<IUserController>[] controllers =
            {
            new UserControllerViaPlatform<IUserController>(RuntimePlatform.Android, () => new MobileController(mono)),
            new UserControllerViaPlatform<IUserController>(RuntimePlatform.WindowsEditor, () => new EditorController(mono))
        };

        creator = new UserControllerCreator<IUserController>(controllers);
    }

    public IUserController GetOrDefault() => creator.GetOrDefault();

    public IUserController GetOrDefault(RuntimePlatform platform) => creator.GetOrDefault(platform);
}
