using Desdiene.Types.Processes;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingAndEnablingGetter
    {
        IProcessGetterNotifier Loading { get; }
        IProcessGetterNotifier Enabling { get; }
    }
}
