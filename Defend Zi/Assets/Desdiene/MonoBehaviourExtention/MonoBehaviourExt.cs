﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Desdiene.Coroutine;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    public class MonoBehaviourExt : MonoBehaviour
    {
        #region Awake realization

        private bool _isAwaked;

        private void Awake()
        {
            TryAwake();
        }

        /// <summary>
        /// Необходимо использовать данный метод в дочернем классе взамен Awake()
        /// </summary>
        protected virtual void AwakeExt() { }

        /// <summary>
        /// Данный метод определяет всю дополнительную логику данного класса.
        /// </summary>
        private void AwakeWrap()
        {
            ForceAwakeSerializeFields();
            AwakeExt();
        }

        /// <summary>
        /// Вызвать Awake у переданного компонента, если он еще не был вызван.
        /// </summary>
        /// <typeparam name="T">Тип компонента.</typeparam>
        /// <param name="component">Компонент, у которого необходимо вызвать AwakeExt.</param>
        /// <returns>Компонент, у которого был вызван AwakeExt.</returns>
        private T TryAwake<T>(T component)
        {
            if (component is MonoBehaviourExt mono)
            {
                // Если компонент пытается проиниализаровать сам себя, например в случае вызова GetComponentsInParent
                if (mono == this) return component;

                mono.TryAwake();
            }

            return component;
        }

        /// <summary>
        /// Вызвать Awake у данного компонента, если он еще не был вызван.
        /// </summary>
        private void TryAwake()
        {
            if (!_isAwaked)
            {
                AwakeWrap();
                _isAwaked = true;
            }
        }

        #endregion


        #region OnEnable realization

        public event Action OnEnabled;

        /// <summary>
        /// Необходимо использовать данный метод в дочернем классе взамен OnEnable()
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
        /// Необходимо использовать данный метод в дочернем классе взамен Start()
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

        public T GetInitedComponent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T component = base.GetComponent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject! Type: {typeof(T)}.");
            }

            return TryAwake(component);
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

            return TryAwake(component);
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

            return TryAwake(component);
        }

        public T[] GetInitedComponentsInParent<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T[] components = base.GetComponentsInParent<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent

            Array.ForEach(components, (component) => TryAwake(component));

            return components;
        }

        public T[] GetInitedComponentsInChildren<T>()
        {
#pragma warning disable UNT0014 // Invalid type for call to GetComponent
            T[] components = GetComponentsInChildren<T>();
#pragma warning restore UNT0014 // Invalid type for call to GetComponent

            Array.ForEach(components, (component) => TryAwake(component));

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

        private const BindingFlags allObjectBinding = BindingFlags.Instance |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;

        /// <summary>
        /// Получить поля с атрибутом SerializeField у текущего объекта.
        /// Учитывает закрытые поля базовых классов.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MonoBehaviourExt> GetSerializeMonoBehaviourExtFields()
        {
            Type monoBehaviourExtType = typeof(MonoBehaviourExt);
            IEnumerable<MonoBehaviourExt> fields = Enumerable.Empty<MonoBehaviourExt>();

            Type type = GetType();
            while (type.IsSubclassOf(monoBehaviourExtType) && type != monoBehaviourExtType)
            {
                fields = fields.Union(GetSerializeMonoBehaviourExtFields(type));
                type = type.BaseType;
            }

            return fields;
        }

        /// <summary>
        /// Получить поля с атрибутом SerializeField у класса с указанным типом.
        /// Не учитывает закрытые поля у базовых классов.
        /// </summary>
        /// <param name="type">Тип класса.</param>
        /// <returns></returns>
        private IEnumerable<MonoBehaviourExt> GetSerializeMonoBehaviourExtFields(Type type)
        {
            return type
                .GetFields(allObjectBinding)
                // false - что атрибут строго SerializeField. Если true, то проверяет и дочерние типы атрибута.
                .Where(field => field.IsDefined(typeof(SerializeField), false))
                .Select(field => field.GetValue(this))
                .OfType<MonoBehaviourExt>();
        }

        private void ForceAwakeSerializeFields()
        {
            IEnumerable<MonoBehaviourExt> serializeMonoBehaviourExtFields = GetSerializeMonoBehaviourExtFields();

            foreach (MonoBehaviourExt component in serializeMonoBehaviourExtFields)
            {
                TryAwake(component);
            }
        }
    }
}
