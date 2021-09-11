using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField, NotNull] private ComponentsProxy componentsProxy;
    [SerializeField, NotNull] private PlayerMono player;

    public override void InstallBindings()
    {
        BindPlayer();
        BindComponentsProxy();
        BindGameManager();
        BindGameDataSaver();
        BindUserInput();
        BindGameDifficulty();
        BindStatisticsCollector();
    }

    private void BindPlayer()
    {
        Container
            .Bind<PlayerMono>()
            .ToSelf()
            .FromInstance(player)
            .AsSingle()
            .NonLazy();
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

    private void BindGameDataSaver()
    {
        Container
            .Bind<GameDataSaver>()
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
    private void BindGameDifficulty()
    {
        Container
            .Bind<GameDifficulty>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindStatisticsCollector()
    {
        Container
            .Bind<GameStatisticsCollector>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }
}
