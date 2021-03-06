using Desdiene.DataSaving.Storages;
using Desdiene.Mobile.GooglePlayApi;
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
        BindGameStatisticsStorage();
        BindGameStatistics();
        BindGameSettingsStorage();
        BindGameSettings();
        BindRewardedAd();
        BindFullScreenWindowsContainer();
        BindScenesInBuild();
        BindLoadedScenes();
        BindGlobalTimeRef();
        BindGlobalTimeScaler();
        BindSingleSceneLoader();
        BindGPGSAuthentication();
        BindGPGSLeaderboard();
        BindScreenOrientation();
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
             .Lazy();
    }

    private void BindRewardedAd()
    {
        Container
            .Bind<IRewardedAd>()
            .To<IronSourceAd>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindFullScreenWindowsContainer()
    {
        Container
            .Bind<FullScreenWindowsContainer>()
            .ToSelf()
            .AsSingle()
            .Lazy();
    }

    private void BindSingleSceneLoader()
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
            .Bind<GpgsLeaderboardMono>()
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
            .Lazy();
    }

    private void BindScreenOrientation()
    {
        Container
            .Bind<IScreenOrientation>()
            .To<ScreenOrientationMono>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameStatisticsStorage()
    {
        Container
            .Bind<IStorageAsync<GameStatisticsDto>>()
            .To<GameStatisticsStorageAsync>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameStatistics()
    {
        Container
            .Bind<IGameStatistics>()
            .To<GameStatistics>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }


    private void BindGameSettingsStorage()
    {
        Container
            .Bind<IStorage<GameSettingsDto>>()
            .To<GameSettingsStorage>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }

    private void BindGameSettings()
    {
        Container
            .Bind<IGameSettings>()
            .To<GameSettings>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .Lazy();
    }
}
