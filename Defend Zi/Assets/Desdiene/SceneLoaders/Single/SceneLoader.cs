﻿using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single.States;
using Desdiene.SceneLoaders.Single.States.Base;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes;
using UnityEngine.SceneManagement;

namespace Desdiene.SceneLoaders.Single
{
    /// <summary>
    /// Need to be a global singleton!
    /// </summary>
    public class SceneLoader : MonoBehaviourExt
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

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

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        public void Load(SceneAsset scene) => Load(scene, null, null);

        public void Load(SceneAsset scene, Action<IProcessesSetter> beforeUnloading, Action afterEnabling)
        {
            CurrentState.Load(scene, beforeUnloading, afterEnabling);
        }

        public void Reload() => Reload(null, null);

        public void Reload(Action<IProcessesSetter> beforeUnloading, Action afterEnabling)
        {
            SceneAsset _sceneToLoad = new SceneAsset(this, SceneManager.GetActiveScene().name);
            Load(_sceneToLoad, beforeUnloading, afterEnabling);
        }
    }
}