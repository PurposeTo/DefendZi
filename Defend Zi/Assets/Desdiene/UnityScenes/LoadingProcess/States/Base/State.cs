using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.CoroutineWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;
using Desdiene.UnityScenes.LoadingProcess.Components;

namespace Desdiene.UnityScenes.LoadingProcess.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint<MutableData>
    {
        private readonly IStateSwitcher<State, MutableData> _stateSwitcher;
        private readonly string _sceneName;
        private readonly ProgressInfo _progressInfo;
        private readonly ICoroutine _stateChecking;
        private string _logMessage = "";

        public State(MonoBehaviourExt mono,
                     IStateSwitcher<State, MutableData> stateSwitcher,
                     AsyncOperation loadingOperation,
                     string sceneName) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (string.IsNullOrEmpty(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            LoadingOperation = loadingOperation ?? throw new ArgumentNullException(nameof(loadingOperation));
            _sceneName = sceneName;
            _progressInfo = new ProgressInfo(loadingOperation);
            _stateChecking = new CoroutineWrap(mono);
        }

        // Вызов осуществляется из дочерних классов
        protected Action onWaitingForAllowingToEnabling;

        public event Action OnWaitingForAllowingToEnabling
        {
            add => onWaitingForAllowingToEnabling += value;
            remove => onWaitingForAllowingToEnabling -= value;
        }

        // Не создавать своё промежуточное событие, так как при одиночной загрузке сцены невозможно дождаться статуса LoadedAndEnabled,
        // т.к. при выгрузке сцены удалится MonoBehaviour объект, а следовательно, и данный класс.
        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        public event Action OnLoadedAndEnabled
        {
            add => LoadingOperation.completed += (_) => value?.Invoke();
            remove => LoadingOperation.completed -= (_) => value?.Invoke();
        }

        public float Progress => LoadingOperation.progress;
        protected AsyncOperation LoadingOperation { get; }

        public abstract void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode enablingMode);

        void IStateEntryExitPoint<MutableData>.OnEnter(MutableData mutableData)
        {
            if (mutableData != null)
            {
                onWaitingForAllowingToEnabling = mutableData.OnWaitingForAllowingToEnabling;
            }

            _logMessage = PrintLoadingLog(_logMessage);
            _stateChecking.StartContinuously(CheckingState());
            OnEnter();
        }

        MutableData IStateEntryExitPoint<MutableData>.OnExit()
        {
            _stateChecking.Terminate();
            OnExit();
            return new MutableData(onWaitingForAllowingToEnabling);
        }

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }

        /// <summary>
        /// Выключить стейт машину. Используется в OnEnter конечных состояний, из которых нельзя выйти.
        /// </summary>
        protected void DisableStateMachine()
        {
            _stateChecking.Terminate();
        }

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();

        /// <summary>
        /// Соответствует ли текущий процесс загрузки данному состоянию?
        /// </summary>
        protected abstract bool IsThisState(ProgressInfo progressInfo);

        protected abstract void FindAndSwitchState(ProgressInfo progressInfo);

        private void CheckState()
        {
            bool isThisState = IsThisState(_progressInfo);
            if (isThisState) _logMessage = PrintLoadingLog(_logMessage);
            else
            {
                try
                {
                    FindAndSwitchState(_progressInfo);
                }
                catch (InvalidOperationException exception)
                {
                    throw new InvalidOperationException($"Unknown loading operation state! "
                                    + $"Progress: {Progress * 100}%, "
                                    + $"allowSceneActivation = {_progressInfo.SceneEnablindAfterLoading}, "
                                    + $"_loadingOperation.isDone = {_progressInfo.IsDone}",
                                      exception);
                }
            }
        }

        private IEnumerator CheckingState()
        {
            while (true)
            {
                Debug.Log($"КРЯ {GetType().Name}");
                CheckState();
                /* При выгрузке сцены удалится MonoBehaviour объект, а с ним данный класс.
                 * Все инструкции должны быть указанны и будут выполнены до yield return инструкции.
                 */
                yield return null;
            }
        }

        private string PrintLoadingLog(string logMessage)
        {
            string newLogMessage = $"Loading scene \"{_sceneName}\". Progress: {Progress * 100}%. State: {GetType().Name}";
            if (logMessage != newLogMessage)
            {
                logMessage = newLogMessage;
                Debug.Log(logMessage);
            }
            return logMessage;
        }
    }
}
