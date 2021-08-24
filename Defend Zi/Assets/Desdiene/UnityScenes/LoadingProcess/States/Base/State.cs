using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.CoroutineWrapper;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingProcess;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingOperationAsset.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        protected AsyncOperation LoadingOperation { get; }
        private readonly string _sceneName;
        private readonly ProgressInfo _progressInfo;
        private readonly ICoroutine _checkingState;

        public State(MonoBehaviourExt mono,
                     IStateSwitcher<State> stateSwitcher,
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
            _checkingState = new CoroutineWrap(mono);
        }

        public float Progress => LoadingOperation.progress;

        public event Action OnWaitingForAllowingToEnabling
        {
            add => onWaitingForAllowingToEnabling += value;
            remove => onWaitingForAllowingToEnabling -= value;
        }
        // Вызов осуществляется из дочерних классов
        protected Action onWaitingForAllowingToEnabling;

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

        public abstract void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode enablingMode);

        public virtual void OnEnter()
        {
            _checkingState.StartContinuously(CheckingState());
        }

        public virtual void OnExit()
        {
            _checkingState.Break();
        }

        /// <summary>
        /// Соответствует ли текущий процесс загрузки данному состоянию?
        /// </summary>
        protected abstract bool IsThisState(ProgressInfo progressInfo);

        private void FindAndSwitchState()
        {
            try
            {
                _stateSwitcher.Switch(state => state.IsThisState(_progressInfo));
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

        private void CheckState()
        {
            Debug.Log($"КРЯ! Проверка актуальности состояния {GetType().Name}");
            bool isNotThisState = !IsThisState(_progressInfo);
            if (isNotThisState)
            {
                FindAndSwitchState();
            }
        }

        private IEnumerator CheckingState()
        {
            string logMessage = "";

            while (true)
            {
                logMessage = PrintLoadingLog(logMessage);
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
