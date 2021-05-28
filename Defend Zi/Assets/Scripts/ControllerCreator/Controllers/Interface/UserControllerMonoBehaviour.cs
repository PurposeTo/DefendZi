using System;
using Desdiene.Singleton;

public class UserControllerMonoBehaviour : SingletonMonoBehaviourExt<UserControllerMonoBehaviour>, IUserController
{
    private IUserController userController;

    public bool IsActive => userController.IsActive;

    public event Action<bool> OnIsActiveChange
    {
        add => userController.OnIsActiveChange += value;
        remove => userController.OnIsActiveChange -= value;
    }

    protected override void AwakeSingleton()
    {
        //вероятно, придется передать монобех в конструктор
        userController = new UserControllerCreator(this).GetOrDefault();
    }
}
