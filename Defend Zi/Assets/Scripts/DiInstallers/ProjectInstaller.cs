using Desdiene.DataStorageFactories.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.TimeControls.Adapters;
using Desdiene.UI.Components;
using Desdiene.UnityScenes;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField, NotNull] private BackgroundMusic _backgroundMusic;
    [SerializeField, NotNull] private TransitionScreen _transitionScreen;

    public override void InstallBindings()
    {
        BindRewardedAd();
        BindFullScreenWindowsContainer();
        BindScenesInBuild();
        BindLoadedScenes();
        BindGlobalTimeRef();
        BindGlobalTimeScaler();
        BingSingleSceneLoader();
        BindGPGSAuthentication();
        BindGPGSLeaderboard();
        BindDataStorage();
        BindBackgroundMusic();
        BindTransitionScreen();
    }

    private void BindBackgroundMusic()
    {
        Container
             .Bind<BackgroundMusic>()
             .ToSelf()
             .FromComponentInNewPrefab(_backgroundMusic)
             .AsSingle()
             .NonLazy();
    }

    private void BindRewardedAd()
    {
        Container
            .Bind<IRewardedAd>()
            //.To<FailRewardedAdStub>()
            .To<IronSourceAd>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindFullScreenWindowsContainer()
    {
        Container
            .Bind<FullScreenWindowsContainer>()
            .AsSingle()
            .Lazy();
    }

    private void BingSingleSceneLoader()
    {
        Container
            .Bind<SceneLoader>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindScenesInBuild()
    {
        Container
            .Bind<ScenesInBuild>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindLoadedScenes()
    {
        Container
            .Bind<LoadedScenes>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindDataStorage()
    {
        Container
            .Bind<IStorage<IGameData>>()
            .To<GameDataStorage>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGPGSAuthentication()
    {
        Container
            .Bind<GpgsAutentification>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGPGSLeaderboard()
    {
        Container
            .Bind<GpgsLeaderboard>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGlobalTimeRef()
    {
        Container
            .Bind<GlobalTimeScaleAdapter>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGlobalTimeScaler()
    {
        Container
            .Bind<ITime>()
            .To<GlobalTime>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindTransitionScreen()
    {
        Container
            .Bind<TransitionScreen>()
            .ToSelf()
            .FromComponentInNewPrefab(_transitionScreen)
            .AsSingle()
            .NonLazy();
    }
}
