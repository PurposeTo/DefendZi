using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes.Loadings;

namespace Desdiene.SceneLoaders.Single.States
{
    public class SceneLoadedAndEnabled : State
    {
        public SceneLoadedAndEnabled(MonoBehaviourExt mono, IStateSwitcher<State> stateSwitcher)
            : base(mono, stateSwitcher) { }

        public override void Load(SceneAsset scene, Action<IProcessesSetter> beforeUnloading, Action afterEnabling)
        {
            if (scene is null) throw new ArgumentNullException(nameof(scene));

            IProcesses beforePastSceneUnloading = new ProcessesContainer("Подготовка к выгрузке старой сцены");
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
