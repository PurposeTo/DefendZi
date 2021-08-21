using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine;
using Desdiene.StateMachine.StateSwitcher;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using Desdiene.UnityScenes.LoadingProcess.States;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingOperationAsset
{
    /// <summary>
    /// Данный класс описывает операцию асинхронной загрузки сцены.
    /// </summary>
    public class LoadingOperation : MonoBehaviourExtContainer, IStateSwitcher<State>
    {
        private readonly string _sceneName;

        private readonly List<State> _allStates;

        public LoadingOperation(MonoBehaviourExt mono, AsyncOperation loadingOperation, string sceneName) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (loadingOperation == null) throw new ArgumentNullException(nameof(loadingOperation));
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _sceneName = sceneName;

            State loading = new Loading(mono, this, loadingOperation, _sceneName);
            _allStates = new List<State>()
            {
                loading,
                new WaitingForAllowingToEnabling(mono, this, loadingOperation, _sceneName),
                new Enabling(mono, this, loadingOperation, _sceneName),
                new LoadedAndEnabled(mono, this, loadingOperation, _sceneName),
        };
            _currentState = loading;
        }

        private State _currentState;

        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены,
        /// либо если данное состояние уже достигнуто.
        /// </summary>
        public event Action OnWaitingForAllowingToEnabling
        {
            add => _currentState.OnWaitingForAllowingToEnabling += value;
            remove => _currentState.OnWaitingForAllowingToEnabling -= value;
        }

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        public event Action OnLoadedAndEnabled
        {
            add => _currentState.OnLoadedAndEnabled += value;
            remove => _currentState.OnLoadedAndEnabled -= value;
        }

        /* Используется слово "enable" для разделения понятий.
         * Согласно документации unity, может быть "active scene" - главная включенная сцена, при использовании LoadSceneMode.Additive
         * И может быть allowSceneActivation - разрешение включение сцены после загрузки.
         */
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// Внимание! Загруженная, но не включенная сцена все равно учитывается unity как загруженная.
        /// </summary>
        public void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode mode) => _currentState.SetAllowSceneEnabling(mode);

        void IStateSwitcher<State>.Switch<ConcreteStateT>()
        {
            var state = _allStates.FirstOrDefault(s => s is ConcreteStateT);
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
    }
}
