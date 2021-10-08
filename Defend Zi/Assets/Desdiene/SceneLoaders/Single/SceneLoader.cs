using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.ProcessContainers;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Types;
using UnityEngine.SceneManagement;
using Zenject;

namespace Desdiene.SceneLoaders.Single
{
    /// <summary>
    /// Need to be a global singleton!
    /// </summary>
    public class SceneLoader : MonoBehaviourExt
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private ScenesInBuild _scenesInBuild;

        [Inject]
        private void Constructor(ScenesInBuild scenesInBuild)
        {
            _scenesInBuild = scenesInBuild ?? throw new ArgumentNullException(nameof(scenesInBuild));
        }

        protected override void AwakeExt()
        {
            StateSwitcher<State> stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new SceneLoadedAndEnabled(this, stateSwitcher),
                new SceneTransition(this, stateSwitcher)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<SceneLoadedAndEnabled>();
        }

        public event Action<IProcessesMutator> BeforeUnloading;
        public event Action AfterEnabling;

        private State CurrentState => _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));

        public void Load(ISceneAsset scene) => Load(scene, BeforeUnloading, AfterEnabling);

        private void Load(ISceneAsset scene, Action<IProcessesMutator> beforeUnloading, Action afterEnabling)
        {
            if (scene == null) throw new ArgumentNullException(nameof(scene));

            CurrentState.Load(scene, beforeUnloading, afterEnabling);
        }

        public void Reload()
        {
            Reload(BeforeUnloading, AfterEnabling);
        }

        private void Reload(Action<IProcessesMutator> beforeUnloading, Action afterEnabling)
        {
            ISceneAsset _sceneToLoad = _scenesInBuild.Get(this, SceneManager.GetActiveScene().name);
            Load(_sceneToLoad, beforeUnloading, afterEnabling);
        }
    }
}
