using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Desdiene.CoroutineWrapper;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    public class MonoBehaviourExt : MonoBehaviour
    {
        #region Awake realization

        private bool _isAwaked;
        private bool _isAwaking;

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
            _isAwaking = true;
            TryAwakeSerializeFields();
            AwakeExt();
            _isAwaking = false;
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
            if (_isAwaking)
            {
                throw new EntryPointNotFoundException($"GameObject \"{gameObject.name}\" with script \"{GetType().Name}\"" +
                    $" is already awaking!");
            }

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

        #region GetComponent

        public T GetInitedComponent<T>()
        {
            T component = base.GetComponent<T>();
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject! Type: {typeof(T)}.");
            }

            return TryAwake(component);
        }

        public T GetInitedComponentInChildren<T>()
        {
            T component = base.GetComponentInChildren<T>();
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject or in children! Type: {typeof(T)}.");
            }

            return TryAwake(component);
        }

        public T GetInitedComponentInParent<T>()
        {
            T component = base.GetComponentInParent<T>();
            if (component == null)
            {
                throw new NullReferenceException($"Can't find component on this gameObject or in parent! Type: {typeof(T)}.");
            }

            return TryAwake(component);
        }

        public T[] GetInitedComponentsInParent<T>()
        {
            T[] components = base.GetComponentsInParent<T>();
            Array.ForEach(components, (component) => TryAwake(component));

            return components;
        }

        public T[] GetInitedComponentsInChildren<T>()
        {
            T[] components = GetComponentsInChildren<T>();
            Array.ForEach(components, (component) => TryAwake(component));

            return components;
        }

        public T GetInitedComponentOnlyInParent<T>()
        {
            try
            {
                T componentOnThisGameObject = base.GetComponent<T>();
                return GetInitedComponentsInParent<T>().Single(it =>
                {
                    return !it.Equals(componentOnThisGameObject);
                });
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
                T componentOnThisGameObject = base.GetComponent<T>();
                return GetInitedComponentsInChildren<T>().Where(it =>
                {
                    return !it.Equals(componentOnThisGameObject);
                }).ToArray();
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

        private void TryAwakeSerializeFields()
        {
            IEnumerable<MonoBehaviourExt> serializeMonoBehaviourExtFields = GetSerializeMonoBehaviourExtFields();

            foreach (MonoBehaviourExt component in serializeMonoBehaviourExtFields)
            {
                TryAwake(component);
            }
        }
    }
}
