using System;
using System.Collections;
using Desdiene.Coroutine.CoroutineExecutor;
using UnityEngine;
using Desdiene.Types.AtomicReference.RefRuntimeInit;

namespace Desdiene.SuperMonoBehaviourAsset
{
    public class SuperMonoBehaviour : MonoBehaviour
    {
        #region SuperMonoBehaviour tools

        private readonly RefRuntimeInit<CoroutineExecutor> coroutineExecutorRef = new RefRuntimeInit<CoroutineExecutor>();

        private void InitSuperMonoBehaviour()
        {
            coroutineExecutorRef.Initialize(InitCoroutineExecutor);
        }

        #endregion


        #region Awake realization

        public event Action OnAwaked
        {
            add
            {
                OnAwakedAction += value;

                if (IsAwaked) ExecuteCommandsAndClear(ref OnAwakedAction);
            }
            remove
            {
                OnAwakedAction -= value;
            }
        }

        private Action OnAwakedAction;
        private bool IsAwaked = false;


        /// <summary>
        /// Необходимо использовать данный метод взамен Awake()
        /// </summary>
        protected virtual void AwakeWrapped() { }

        private void EndAwakeExecution()
        {
            IsAwaked = true;
            ExecuteCommandsAndClear(ref OnAwakedAction);
        }

        private void AwakeSuper()
        {
            InitSuperMonoBehaviour();
            AwakeWrapped();
            EndAwakeExecution();
        }

        private void Awake()
        {
            AwakeSuper();
        }

        #endregion


        #region OnEnable realization

        public event Action OnEnabed;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnEnable()
        /// </summary>
        protected virtual void OnEnableWrapped() { }

        private void EndOnEnableExecution()
        {
            //UpdateManager.AddUpdate(UpdateSuper);
            //UpdateManager.AddFixedUpdate(FixedUpdateSuper);
            //UpdateManager.AddLateUpdate(LateUpdateSuper);
            OnEnabed?.Invoke();
        }

        private void OnEnableSuper()
        {
            OnEnableWrapped();
            EndOnEnableExecution();
        }

        private void OnEnable()
        {
            OnEnableSuper();
        }

        #endregion


        #region Start realization

        public event Action OnStarted
        {
            add
            {
                OnStartedAction += value;

                if (IsStarted) ExecuteCommandsAndClear(ref OnStartedAction);
            }
            remove
            {
                OnStartedAction -= value;
            }
        }

        private Action OnStartedAction;
        private bool IsStarted = false;


        /// <summary>
        /// Необходимо использовать данный метод взамен Start()
        /// </summary>
        protected virtual void StartWrapped() { }

        private void EndStartExecution()
        {
            IsStarted = true;
            ExecuteCommandsAndClear(ref OnStartedAction);
        }

        private void StartSuper()
        {
            StartWrapped();
            EndStartExecution();
        }

        private void Start()
        {
            StartSuper();
        }

        #endregion


        #region OnDisable realization

        public event Action OnDisabled;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnEnable()
        /// </summary>
        protected virtual void OnDisableWrapped() { }

        private void EndOnDisableExecution()
        {
            //UpdateManager.RemoveUpdate(UpdateSuper);
            //UpdateManager.RemoveFixedUpdate(FixedUpdateSuper);
            //UpdateManager.RemoveLateUpdate(LateUpdateSuper);
            OnDisabled?.Invoke();
        }

        private void OnDisableSuper()
        {
            OnDisableWrapped();
            EndOnDisableExecution();
        }

        private void OnDisable()
        {
            OnDisableSuper();
        }

        #endregion


        #region OnDestroy realization

        public event Action OnDestroyed;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnDestroy()
        /// </summary>
        protected virtual void OnDestroyWrapped() { }

        private void OnDestroySuper()
        {
            OnDestroyWrapped();
            EndOnDestroyExecution();
        }


        private void EndOnDestroyExecution()
        {
            OnDestroyed?.Invoke();
        }


        private void OnDestroy()
        {
            OnDestroySuper();
        }

        #endregion


        #region Update realization

        /// <summary>
        /// Необходимо использовать данный метод взамен Update()
        /// </summary>
        protected virtual void UpdateWrapped() { }

        private void UpdateSuper()
        {
            UpdateWrapped();
        }

        #endregion


        #region FixedUpdate realization

        /// <summary>
        /// Необходимо использовать данный метод взамен FixedUpdate()
        /// </summary>
        protected virtual void FixedUpdateWrapped() { }

        private void FixedUpdateSuper()
        {
            FixedUpdateWrapped();
        }

        #endregion


        #region LateUpdate realization

        /// <summary>
        /// Необходимо использовать данный метод взамен LateUpdate()
        /// </summary>
        protected virtual void LateUpdateWrapped() { }

        private void LateUpdateSuper()
        {
            LateUpdateWrapped();
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
        public ICoroutineContainer CreateCoroutineContainer()
        {
            return GetCoroutineExecutor().CreateCoroutineContainer();
        }

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        public void ExecuteCoroutineContinuously(ICoroutineContainer coroutineInfo, IEnumerator enumerator)
        {
            coroutineExecutorRef.Get(InitCoroutineExecutor).ExecuteCoroutineContinuously(coroutineInfo, enumerator);
        }

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        public void ReStartCoroutineExecution(ICoroutineContainer coroutineInfo, IEnumerator enumerator)
        {
            GetCoroutineExecutor().ReStartCoroutineExecution(coroutineInfo, enumerator);
        }

        /// <summary>
        /// Останавливает корутину.
        /// </summary>
        public void BreakCoroutine(ICoroutineContainer coroutineInfo)
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
            return coroutineExecutorRef.Get(InitCoroutineExecutor);
        }

        private CoroutineExecutor InitCoroutineExecutor()
        {
            return new CoroutineExecutor(this);
        }

        #endregion
    }
}
