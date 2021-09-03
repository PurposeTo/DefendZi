using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneLoaders.Single.States.Components;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using UnityEngine;

namespace Desdiene.SceneLoaders.Single.States
{
    public class SceneTransition : State
    {
        public SceneTransition(MonoBehaviourExt mono, IStateSwitcher<State, StateContext> stateSwitcher) : base(mono, stateSwitcher)
        { }

        public override void Load(SceneAsset scene)
        {
            Debug.LogError($"Scene transition is in progress now. You can't load scene.");
        }

        public override void Reload()
        {
            Debug.LogError($"Scene transition is in progress now. You can't reload scene.");
        }

        protected override void OnEnter()
        {
            IProcesses pastSceneUnloadingPreparation = new ProcessesContainer("Подготовка к выгрузке старой сцены");
            OnSceneLoading(pastSceneUnloadingPreparation);
            ILoadingAndEnabling loading = SceneToLoad.LoadAsSingle(SceneEnablingAfterLoading.Mode.Forbid);

            new TransitionProcess(loading, pastSceneUnloadingPreparation).OnReadyToChangeScene += () =>
            {
                loading.SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode.Allow);
            };

            loading.OnLoadedAndEnabled += OnSceneLoadedAndEnabled;
        }

        private void OnSceneLoading(IProcessesSetter pastSceneUnloadingPreparation)
        {
            // Остановить время
            // Начать закрывать заслонку. Добавить закрытие заслонки в ожидание выполнения.
        }

        private void OnSceneLoadedAndEnabled(ILoadingAndEnablingNotifier loadingNotifier)
        {
            // Включить время
            // Начать открывать заслонку, не дожидаясь окончания действия
            SwitchState<Default>();
        }
    }
}
