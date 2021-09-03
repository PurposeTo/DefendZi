using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States.Base;
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
            OnSceneLoading();
            ILoading loading = SceneToLoad.LoadAsSingle(SceneEnablingAfterLoading.Mode.Forbid);
            loading.OnLoadedAndEnabled += OnSceneLoadedAndEnabled;

            IProcess nextSceneLoading = new Process("Загрузка новой сцены", true);
            IProcess previousSceneUnloadingPreparation  = new Process("Подготовка к выгрузке старой сцены", true);

            void OnSceneLoaded(ILoadingNotifier loadingNotifier)
            {
                nextSceneLoading.Set(false);
                loadingNotifier.OnLoaded -= OnSceneLoaded;
            }
            loading.OnLoaded += OnSceneLoaded;

            void OnPreviousScenePreparedToUnload(IMutableProcessGetter process)
            {
                if (!process.KeepWaiting)
                {
                    loading.SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode.Allow);
                    process.OnChanged -= OnPreviousScenePreparedToUnload;
                }
            }
            List<IProcess> processesList = new List<IProcess>()
            {
                nextSceneLoading,
                previousSceneUnloadingPreparation
            };
            new ProcessesContainer("Загрузка и подготовка сцены", processesList).OnChanged += OnPreviousScenePreparedToUnload;
        }

        private void OnSceneLoading()
        {
            // Остановить время
            // Начать закрывать заслонку. Добавить закрытие заслонки в ожидание выполнения.
        }

        private void OnSceneLoadedAndEnabled(ILoadingNotifier loadingNotifier)
        {
            // Включить время
            // Начать открывать заслонку, не дожидаясь окончания действия
            SwitchState<Default>();
        }
    }
}
