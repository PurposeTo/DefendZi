using System;
using System.Collections;
using Desdiene.Coroutine.CoroutineExecutor;
using UnityEngine;
using Desdiene.Types.AtomicReference.RefRuntimeInit;
using Desdiene.Types.EventContainers;

namespace Desdiene.MonoBehaviourExtention
{
    public class MonoBehaviourExt : MonoBehaviour
    {
        #region SuperMonoBehaviour tools

        private readonly RefRuntimeInit<CoroutineExecutor> coroutineExecutorRef = new RefRuntimeInit<CoroutineExecutor>();

        private void InitSuperMonoBehaviour()
        {
            coroutineExecutorRef.GetOrInit(InitCoroutineExecutor);
        }

        #endregion

        protected void AwakeWrapped(
            IInitionEvent<MonoBehaviourExt>[] eventWraps,
            Action awakeBlock)
        {
            //fixme написал херню. Необходимо вкладывать каждое следующее событие в предыдущее. 
            //А в конце в это все вложить AwakeBlock.

            //Array.ForEach(eventWraps, (eventWrap) =>
            //{
            //    firstEventWrap.Event += (inst) =>
            //    {
            //        eventWrap.Event += (_) => { };
            //    };
            //});
            //firstEventWrap.Event += (_) => AwakeBlock?.Invoke();
        }


        #region Awake realization


        /// <summary>
        /// Событие вызовется после выполнения метода Awake объекта или сразу же, если Awake уже был вызван.
        /// </summary>
        public event Action OnIsAwaked
        {
            add
            {
                if (IsAwaked) value?.Invoke();
                else OnIsAwakedAction += value;
            }
            remove
            {
                OnIsAwakedAction -= value;
            }
        }

        private Action OnIsAwakedAction;
        private bool IsAwaked = false;

        /// <summary>
        /// Необходимо использовать данный метод взамен Awake()
        /// </summary>
        protected virtual void AwakeExt() { }

        private void EndAwakeExecution()
        {
            IsAwaked = true;
            ExecuteCommandsAndClear(ref OnIsAwakedAction);
        }

        private void Awake()
        {
            InitSuperMonoBehaviour();
            AwakeExt();
            EndAwakeExecution();
        }

        #endregion


        #region OnEnable realization

        public event Action OnEnabed;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnEnable()
        /// </summary>
        protected virtual void OnEnableExt() { }

        private void EndOnEnableExecution()
        {
            //UpdateManager.AddUpdate(UpdateSuper);
            //UpdateManager.AddFixedUpdate(FixedUpdateSuper);
            //UpdateManager.AddLateUpdate(LateUpdateSuper);
            OnEnabed?.Invoke();
        }

        private void OnEnable()
        {
            OnEnableExt();
            EndOnEnableExecution();
        }

        #endregion


        #region Start realization

        /// <summary>
        /// Событие вызовется после выполнения метода Start объекта или сразу же, если Start уже был вызван.
        /// </summary>
        public event Action OnIsStarted
        {
            add
            {
                if (IsStarted) value?.Invoke();
                else OnIsStartedAction += value;
            }
            remove
            {
                OnIsStartedAction -= value;
            }
        }

        private Action OnIsStartedAction;
        private bool IsStarted = false;


        /// <summary>
        /// Необходимо использовать данный метод взамен Start()
        /// </summary>
        protected virtual void StartExt() { }

        private void EndStartExecution()
        {
            IsStarted = true;
            ExecuteCommandsAndClear(ref OnIsStartedAction);
        }

        private void Start()
        {
            StartExt();
            EndStartExecution();
        }

        #endregion


        #region OnDisable realization

        public event Action OnDisabled;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnEnable()
        /// </summary>
        protected virtual void OnDisableExt() { }

        private void EndOnDisableExecution()
        {
            //UpdateManager.RemoveUpdate(UpdateSuper);
            //UpdateManager.RemoveFixedUpdate(FixedUpdateSuper);
            //UpdateManager.RemoveLateUpdate(LateUpdateSuper);
            OnDisabled?.Invoke();
        }

        private void OnDisable()
        {
            OnDisableExt();
            EndOnDisableExecution();
        }

        #endregion


        #region OnDestroy realization

        public event Action OnDestroyed;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnDestroy()
        /// </summary>
        protected virtual void OnDestroyExt() { }

        private void EndOnDestroyExecution()
        {
            OnDestroyed?.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyExt();
            EndOnDestroyExecution();
        }

        #endregion


        #region Update realization

        /// <summary>
        /// Необходимо использовать данный метод взамен Update()
        /// </summary>
        protected virtual void UpdateExt() { }

        private void UpdateSuper()
        {
            UpdateExt();
        }

        #endregion


        #region FixedUpdate realization

        /// <summary>
        /// Необходимо использовать данный метод взамен FixedUpdate()
        /// </summary>
        protected virtual void FixedUpdateExt() { }

        private void FixedUpdateSuper()
        {
            FixedUpdateExt();
        }

        #endregion


        #region LateUpdate realization

        /// <summary>
        /// Необходимо использовать данный метод взамен LateUpdate()
        /// </summary>
        protected virtual void LateUpdateExt() { }

        private void LateUpdateSuper()
        {
            LateUpdateExt();
        }

        #endregion


        protected void ExecuteCommandsAndClear(ref Action action)
        {
            action?.Invoke();
            action = null;
        }

        #region CoroutineExecutor

        /// <summary>
        /// Создаёт "Container" объект для конкретной корутины
        /// </summary>
        public ICoroutine CreateCoroutine()
        {
            return GetCoroutineExecutor().CreateCoroutineContainer();
        }

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        public void ExecuteCoroutineContinuously(ICoroutine coroutineInfo, IEnumerator enumerator)
        {
            coroutineExecutorRef.GetOrInit(InitCoroutineExecutor).ExecuteCoroutineContinuously(coroutineInfo, enumerator);
        }

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        public void ReStartCoroutineExecution(ICoroutine coroutineInfo, IEnumerator enumerator)
        {
            GetCoroutineExecutor().ReStartCoroutineExecution(coroutineInfo, enumerator);
        }

        /// <summary>
        /// Останавливает корутину.
        /// </summary>
        public void BreakCoroutine(ICoroutine coroutineInfo)
        {
            GetCoroutineExecutor().BreakCoroutine(coroutineInfo);
        }

        /// <summary>
        /// Останавливает все корутины на объекте
        /// </summary>
        public void BreakAllCoroutines()
        {
            GetCoroutineExecutor().BreakAllCoroutines();
        }

        private CoroutineExecutor GetCoroutineExecutor()
        {
            return coroutineExecutorRef.GetOrInit(InitCoroutineExecutor);
        }

        private CoroutineExecutor InitCoroutineExecutor()
        {
            return new CoroutineExecutor(this);
        }

        #endregion
    }
}
