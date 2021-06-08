using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private ComponentsProxy componentsProxy;

    public override void InstallBindings()
    {
        BindComponentsProxy();
        BindGameManager();
        BindUserInput();
    }

    private void BindComponentsProxy()
    {
        Container
            .Bind<ComponentsProxy>()
            .ToSelf()
            .FromInstance(componentsProxy)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameManager()
    {
        Container
            .Bind<GameManager>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
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
