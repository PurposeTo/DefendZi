using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUserInput();
    }

    private void BindUserInput()
    {
        Container
            .Bind<IUserInput>()
            .To<UserInputMono>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }
}
