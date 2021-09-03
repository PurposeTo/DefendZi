using System;
using System.Collections.Generic;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes.Loadings;

namespace Desdiene.SceneLoaders.Single.States.Components
{
    public class TransitionProcess
    {
        private readonly IProcess _nextSceneLoading;
        private readonly IMutableProcessGetter _pastSceneUnloadingPreparation;

        public TransitionProcess(ILoadingAndEnabling nextSceneLoading, IMutableProcessGetter pastSceneUnloadingPreparation)
        {
            if (nextSceneLoading is null) throw new ArgumentNullException(nameof(nextSceneLoading));

            _nextSceneLoading = new Process("Загрузка новой сцены", true);
            _pastSceneUnloadingPreparation = pastSceneUnloadingPreparation ?? throw new ArgumentNullException(nameof(pastSceneUnloadingPreparation));

            List<IMutableProcessGetter> processesList = new List<IMutableProcessGetter>()
            {
                _nextSceneLoading,
                _pastSceneUnloadingPreparation
            };
            IMutableProcessGetter sceneTransition = new ProcessesContainer("Загрузка и подготовка сцены", processesList);
            sceneTransition.OnChanged += CheckTransitionProcess;
            nextSceneLoading.OnLoaded += OnNextSceneLoaded;
            _pastSceneUnloadingPreparation.OnChanged += CheckPastScenePreparedToUnload;
            CheckTransitionProcess(sceneTransition);
        }

        public event Action OnReadyToChangeScene;

        private void OnNextSceneLoaded(IMutableLoadingAndEnablingGetter loading)
        {
            _nextSceneLoading.Set(false);
            loading.OnLoaded -= OnNextSceneLoaded;
        }

        private void CheckPastScenePreparedToUnload(IMutableProcessGetter process)
        {
            if (!process.KeepWaiting)
            {
                process.OnChanged -= CheckPastScenePreparedToUnload;
            }
        }

        private void CheckTransitionProcess(IMutableProcessGetter process)
        {
            if (!process.KeepWaiting)
            {
                OnReadyToChangeScene?.Invoke();
                process.OnChanged -= CheckTransitionProcess;
            }
        }
    }
}
