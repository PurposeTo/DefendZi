using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine;
using Desdiene.StateMachine.StateSwitcher;
using Desdiene.UnityScenes.LoadingProcess;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingOperationAsset.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly string _sceneName;
        private readonly ICoroutine _checkingStatus;
        protected AsyncOperation LoadingOperation { get; }
        protected ProgressInfo ProgressInfo { get; }

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
            ProgressInfo = new ProgressInfo(loadingOperation);
            _checkingStatus = new CoroutineWrap(mono);
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
            _checkingStatus.StartContinuously(CheckingStatus());
        }

        public virtual void OnExit()
        {
            _checkingStatus.Break();
        }

        protected void SwitchState<stateT>() where stateT : State
        {
            _stateSwitcher.Switch<stateT>();
        }

        //todo: сделать так, чтобы он проверял, соответствует ли текущее ожидаемое состояние текущему фактическому состоянию.
        // Если нет, то сменить состояние.
        /// <summary>
        /// Данный метод выполняется при проверке состояния прогресса загрузки сцены.
        /// </summary>
        protected abstract void OnCheckingState();

        private IEnumerator CheckingStatus()
        {
            string logMessage = "";

            while (true)
            {
                logMessage = PrintLoadingLog(logMessage, this);
                OnCheckingState();
                /* При выгрузке сцены удалится MonoBehaviour объект, а следовательно, и данный класс.
                 * Все инструкции должны быть указанны и будут выполнены до yield return инструкции.
                 */
                yield return null;
            }
        }

        private string PrintLoadingLog(string logMessage, State state)
        {
            string newLogMessage = $"Loading scene \"{_sceneName}\". Progress: {Progress * 100}%. State: {state.GetType().Name}";
            if (logMessage != newLogMessage)
            {
                logMessage = newLogMessage;
                Debug.Log(logMessage);
            }
            return logMessage;
        }
    }
}
