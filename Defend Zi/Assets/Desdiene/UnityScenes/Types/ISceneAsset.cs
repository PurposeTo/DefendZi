using System;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Loadings;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes.Types
{
    public interface ISceneAsset
    {
        ILoadingAndEnabling LoadAsSingle(Action<ILinearProcessesMutator> beforeEnabling);
        ILoadingAndEnabling LoadAsAdditive(Action<ILinearProcessesMutator> beforeEnabling);
        ILoadingAndEnabling Load(LoadSceneMode loadSceneMode, Action<ILinearProcessesMutator> beforeEnabling);
    }
}
