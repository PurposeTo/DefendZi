using Assets.Desdiene.GooglePlayApi;
using Desdiene.TimeControl.Pausable;
using Desdiene.TimeControl.Scale;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGlobalTimeScaler();
        BindGlobalTimePauser();
        BindGPGSAuthentication();
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
