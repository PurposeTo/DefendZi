using System;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Loadings;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes.Types
{
    public interface ISceneAsset
    {
        ILoadingAndEnabling LoadAsSingle(Action<IProcessesMutator> beforeEnabling);
        ILoadingAndEnabling LoadAsAdditive(Action<IProcessesMutator> beforeEnabling);
        ILoadingAndEnabling Load(LoadSceneMode loadSceneMode, Action<IProcessesMutator> beforeEnabling);
    }
}
