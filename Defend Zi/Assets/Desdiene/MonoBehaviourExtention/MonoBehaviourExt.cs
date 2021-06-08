﻿using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.Coroutine;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtention
{
    public class MonoBehaviourExt : MonoBehaviour
    {
        #region Awake realization

        /// <summary>
        /// Необходимо использовать данный метод взамен Awake()
        /// </summary>
        protected virtual void Constructor() { }

        private void Awake()
        {
            Constructor();
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
        /// Необходимо использовать данный метод взамен Start()
        /// </summary>
        protected virtual void StartExt() { }

        private void Start()
        {
            StartExt();
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


        #region GetComponent

        public new T GetComponent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            return component ?? throw new NullReferenceException($"Can't find component! Type: {typeof(T)}.");
        }

        public new T GetComponentInChildren<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponentInChildren<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            return component ?? throw new NullReferenceException($"Can't find component in children! Type: {typeof(T)}.");
        }

        public new T GetComponentInParent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponentInParent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            return component ?? throw new NullReferenceException($"Can't find component in parent! Type: {typeof(T)}.");
        }

        public T GetComponentOnlyInParent<T>()
        {
            try
            {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
                return GetComponentsInParent<T>().Single(it => !it.Equals(this));
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            }
            catch (InvalidOperationException exception)
            {
                throw new NullReferenceException($"Not found {typeof(T)} on parent gameObject/s!", exception);
            }
        }

        #endregion
    }
}
