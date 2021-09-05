using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;

namespace Desdiene.SceneLoaders.Single.States
{
    public class SceneLoadedAndEnabled : State
    {
        public SceneLoadedAndEnabled(MonoBehaviourExt mono, IStateSwitcher<State> stateSwitcher)
            : base(mono, stateSwitcher) { }

        public override void Load(SceneAsset scene, Action<IProcessesSetter> beforeUnloading, Action afterEnabling)
        {
            if (scene is null) throw new ArgumentNullException(nameof(scene));

            SwitchState<SceneTransition>();
            IProcesses beforePastSceneUnloading = new ProcessesContainer("Подготовка к выгрузке старой сцены");
            beforeUnloading?.Invoke(beforePastSceneUnloading);
            ILoadingAndEnabling loadingAndEnabling = scene.LoadAsSingle(SceneEnablingAfterLoading.Mode.Forbid);
            IProcessGetterNotifier nextSceneLoadingProcess = loadingAndEnabling.Loading;

            List<IProcessGetterNotifier> processesList = new List<IProcessGetterNotifier>()
            {
                nextSceneLoadingProcess,
                beforePastSceneUnloading
            };
            IProcessGetterNotifier sceneTransition = new ProcessesContainer("Подготовка перехода на следующую сцену", processesList);

            void OnSceneTransitionCompleted()
            {
                loadingAndEnabling.AllowSceneEnabling();
                sceneTransition.OnCompleted -= OnSceneTransitionCompleted;
            }

            sceneTransition.OnCompleted += OnSceneTransitionCompleted;

            void OnSceneLoadedAndEnabled()
            {
                afterEnabling?.Invoke();
                SwitchState<SceneLoadedAndEnabled>();
                loadingAndEnabling.OnLoadedAndEnabled -= OnSceneLoadedAndEnabled;
            }

            loadingAndEnabling.OnLoadedAndEnabled += OnSceneLoadedAndEnabled;
        }
    }
}
