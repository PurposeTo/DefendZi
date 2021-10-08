using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Types;

namespace Desdiene.SceneLoaders.Single.States
{
    public class SceneLoadedAndEnabled : State
    {
        public SceneLoadedAndEnabled(MonoBehaviourExt mono, IStateSwitcher<State> stateSwitcher)
            : base(mono, stateSwitcher) { }

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
