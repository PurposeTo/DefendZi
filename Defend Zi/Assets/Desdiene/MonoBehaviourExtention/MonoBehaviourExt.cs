using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.Coroutine;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    public class MonoBehaviourExt : MonoBehaviour
    {
        #region Awake realization

        private bool _isAwaked;

        /// <summary>
        /// Необходимо использовать данный метод взамен Awake()
        /// </summary>
        protected virtual void AwakeExt() { }

        private void TryAwakeExt()
        {
            if (!_isAwaked)
            {
                AwakeExt();
                _isAwaked = true;
            }
        }

        private void Awake()
        {
            TryAwakeExt();
        }

        #endregion


        #region OnEnable realization

        public event Action OnEnabled;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnEnable()
        /// </summary>
        protected virtual void OnEnableExt() { }

        private void EndOnEnableExecution()
        {
            OnEnabled?.Invoke();
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

        private T ForceAwakeExt<T>(T component)
        {
            if (component is MonoBehaviourExt mono)
            {
                // Если компонент пытается проиниализаровать сам себя, например в случае вызова GetComponentsInParent
                if (mono == this) return component;

                mono.TryAwakeExt();
            }

            return component;
        }

        public T GetInitedComponent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject! Type: {typeof(T)}.");
            }

            return ForceAwakeExt(component);
        }

        public T GetInitedComponentInChildren<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponentInChildren<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject or in children! Type: {typeof(T)}.");
            }

            return ForceAwakeExt(component);
        }

        public T GetInitedComponentInParent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponentInParent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject or in parent! Type: {typeof(T)}.");
            }

            return ForceAwakeExt(component);
        }

        public T[] GetInitedComponentsInParent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T[] components = base.GetComponentsInParent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent

            Array.ForEach(components, (component) => ForceAwakeExt(component));

            return components;
        }

        public T[] GetInitedComponentsInChildren<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T[] components = GetComponentsInChildren<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent

            Array.ForEach(components, (component) => ForceAwakeExt(component));

            return components;
        }

        public T GetInitedComponentOnlyInParent<T>()
        {
            try
            {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
                T componentOnThisGameObject = base.GetComponent<T>();
                return GetInitedComponentsInParent<T>().Single(it =>
                {
                    return !it.Equals(componentOnThisGameObject);
                });
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            }
            catch (InvalidOperationException exception)
            {
                throw new NullReferenceException($"Can't find component in parent! Type: {typeof(T)}.", exception);
            }
        }

        public T[] GetComponentsOnlyInChildren<T>()
        {
            try
            {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
                T componentOnThisGameObject = base.GetComponent<T>();
                return GetInitedComponentsInChildren<T>().Where(it =>
                {
                    return !it.Equals(componentOnThisGameObject);
                }).ToArray();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            }
            catch (InvalidOperationException exception)
            {
                throw new NullReferenceException($"Can't find component in childrens! Type: {typeof(T)}.", exception);
            }
        }

        #endregion
    }
}
