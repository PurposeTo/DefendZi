using Desdiene.DataStorageFactories.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.SceneLoaders.Single;
using Desdiene.TimeControls;
using Desdiene.TimeControls.Adapters;
using Desdiene.UI.Components;
using Desdiene.UnityScenes;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
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
    }

    private void BindRewardedAd()
    {
        Container
            .Bind<IRewardedAd>()
            .To<FailRewardedAdStub>()
            //.To<IronSourceAd>()
            //.FromNewComponentOnNewGameObject()
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
            .To<DataStorage>()
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
}
