using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Types;

namespace Desdiene.SceneLoaders.Single
{
    public partial class SceneLoader
    {
        private class SceneLoadedAndEnabled : State
        {
            public SceneLoadedAndEnabled(MonoBehaviourExt mono, SceneLoader it) : base(mono, it) { }

            public override void Load(ISceneAsset scene, Action<IProcessesMutator> beforeUnloading, Action afterEnabling)
            {
                if (scene is null) throw new ArgumentNullException(nameof(scene));

                IProcessesMutator beforePastSceneUnloading = new ParallelProcesses("Подготовка к выгрузке старой сцены");
                ILoadingAndEnabling loadingAndEnabling = scene.LoadAsSingle(beforeUnloading);

                void OnSceneLoadedAndEnabled()
                {
                    afterEnabling?.Invoke();
                    SwitchState<SceneLoadedAndEnabled>();
                    loadingAndEnabling.OnLoadedAndEnabled -= OnSceneLoadedAndEnabled;
                }

                SwitchState<SceneTransition>();
                loadingAndEnabling.OnLoadedAndEnabled += OnSceneLoadedAndEnabled;
            }
        }
    }
}
