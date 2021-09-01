﻿using System;
using System.Collections.Generic;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.UnityScenes.Loadings.States.Base;
using Desdiene.UnityScenes.Loadings.States;
using UnityEngine;
using Desdiene.UnityScenes.Loadings.Components;

namespace Desdiene.UnityScenes.Loadings
{
    /// <summary>
    /// Данный класс описывает операцию асинхронной загрузки сцены.
    /// </summary>
    public class Loading : MonoBehaviourExtContainer, ILoading
    {
        private readonly string _sceneName;
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public Loading(MonoBehaviourExt mono, AsyncOperation loadingOperation, string sceneName) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (loadingOperation == null) throw new ArgumentNullException(nameof(loadingOperation));
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _sceneName = sceneName;

            StateSwitcher<State, MutableData> stateSwitcher = new StateSwitcher<State, MutableData>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new States.Loading(mono, stateSwitcher, loadingOperation, _sceneName),
                new WaitingForAllowingToEnabling(mono, stateSwitcher, loadingOperation, _sceneName),
                new Enabling(mono, stateSwitcher, loadingOperation, _sceneName),
                new LoadedAndEnabled(mono, stateSwitcher, loadingOperation, _sceneName)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<States.Loading>();
        }

        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        public event Action OnWaitingForAllowingToEnabling
        {
            add => CurrentState.OnWaitingForAllowingToEnabling += value;
            remove => CurrentState.OnWaitingForAllowingToEnabling -= value;
        }

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        public event Action OnLoadedAndEnabled
        {
            add => CurrentState.OnLoadedAndEnabled += value;
            remove => CurrentState.OnLoadedAndEnabled -= value;
        }

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        /* Используется слово "enable" для разделения понятий.
         * Согласно документации unity, может быть "active scene" - главная включенная сцена, при использовании LoadSceneMode.Additive
         * И может быть allowSceneActivation - разрешение включение сцены после загрузки.
         */
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// Внимание! Загруженная, но не включенная сцена все равно учитывается unity как загруженная.
        /// </summary>
        public void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode mode) => CurrentState.SetAllowSceneEnabling(mode);
    }
}