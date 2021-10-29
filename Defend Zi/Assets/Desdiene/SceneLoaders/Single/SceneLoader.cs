using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
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
    public partial class SceneLoader : MonoBehaviourExt
    {
        private IStateSwitcher<State> _stateSwitcher;
        private ScenesInBuild _scenesInBuild;

        [Inject]
        private void Constructor(ScenesInBuild scenesInBuild)
        {
            _scenesInBuild = scenesInBuild ?? throw new ArgumentNullException(nameof(scenesInBuild));
        }

        protected override void AwakeExt()
        {
            State initState = new SceneLoadedAndEnabled(this, this);
            List<State> allStates = new List<State>()
            {
                initState,
                new SceneTransition(this, this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);

        }

        /// <summary>
        /// Перед выгрузкой сцены
        /// </summary>
        public event Action<IProcessesMutator> BeforeUnloading;

        /// <summary>
        /// После загрузки сцены
        /// </summary>
        public event Action AfterEnabling;

        private State CurrentState => _stateSwitcher.CurrentState;

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
