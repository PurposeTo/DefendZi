using System;
using System.Collections.Generic;
using Desdiene.Coroutine;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtention
{
    public class MonoBehaviourExt : MonoBehaviour
    {
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

        private readonly List<ICoroutine> activeCoroutines = new List<ICoroutine>();

        /// <summary>
        /// Добавить выполняемую корутину в общий список для отслеживания.
        /// Данный метод выполняется ТОЛЬКО CoroutineWrap-ом.
        /// </summary>
        /// <param name="coroutine"></param>
        public void AddCoroutine(ICoroutine coroutine)
        {
            if (coroutine.IsExecuting)
            {
                coroutine.OnStopped += () => activeCoroutines.Remove(coroutine);
                activeCoroutines.Add(coroutine);
            }
            else throw new ArgumentException("Добавить можно только выполняемую корутину!");
        }

        /// <summary>
        /// Остановить все корутины на объекте.
        /// Архитектура и логика поведения взята из UnityEngine 
        /// </summary>
        public void BreakAllCoroutines()
        {
            for (int i = 0; i < activeCoroutines.Count; i++)
            {
                activeCoroutines[i].Break();
            }
        }

        #endregion
    }
}
