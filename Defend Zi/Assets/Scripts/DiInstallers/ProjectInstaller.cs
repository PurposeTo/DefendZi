using Desdiene.DataStorageFactories.Storages;
using Desdiene.GooglePlayApi;
using Desdiene.TimeControls.Pausables;
using Desdiene.TimeControls.Scales;
using Desdiene.UnityScenes;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindScenesInBuild();
        BindLoadedScenes();
        BindGlobalTimeScaler();
        BindGlobalTimePauser();
        BindGPGSAuthentication();
        BindDataStorage();
    }

    private void BindScenesInBuild()
    {
        Container
            .Bind<ScenesInBuild>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindLoadedScenes()
    {
        Container
            .Bind<LoadedScenes>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindDataStorage()
    {
        Container
            .Bind<IStorage<IGameData>>()
            .To<DataStorage>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindGPGSAuthentication()
    {
        Container
            .Bind<IGPGSAuthentication>()
            .To<GPGSAuthentication>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindGlobalTimeScaler()
    {
        Container
            .Bind<GlobalTimeScaler>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }

    private void BindGlobalTimePauser()
    {
        Container
            .Bind<GlobalTimePausable>()
            .ToSelf()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
    }
}
