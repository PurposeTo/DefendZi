﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace Desdiene.MonoBehaviourExtension
{
    public class MonoBehaviourExt : MonoBehaviour, IUpdateRunner
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
        /// Внутри данного метода и следующих, которые соотносятся с ЖЦ gameObject-а можно безопасно обращаться к SerializeField полям данного класса
        /// </summary>
        protected virtual void AwakeExt() { }

        /// <summary>
        /// Данный метод определяет всю дополнительную логику данного класса.
        /// </summary>
        private void AwakeWrap()
        {
            _isAwaking = true;
            MonoBehaviourExt[] bindedFieldsComponents = GetSerializeMonoBehaviourExtFields().ToArray();
            TryAwake(bindedFieldsComponents);
            AwakeExt();
            _isAwaking = false;
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

        private T[] TryAwake<T>(T[] components)
        {
            return components.Select(component => TryAwake(component)).ToArray();
        }

        #endregion


        #region OnEnable realization

        public event Action OnEnabled;

        /// <summary>
        /// Необходимо использовать данный метод в дочернем классе взамен OnEnable()
        /// </summary>
        protected virtual void OnEnableExt() { }

        private void OnEnable()
        {
            OnEnableExt();
            AddUpdates();
            OnEnabled?.Invoke();
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

        private void OnDisable()
        {
            OnDisableExt();
            RemoveUpdates();
            OnDisabled?.Invoke();
        }

        #endregion


        #region OnDestroy realization

        public event Action OnDestroyed;

        /// <summary>
        /// Необходимо использовать данный метод взамен OnDestroy()
        /// </summary>
        protected virtual void OnDestroyExt() { }

        private void OnDestroy()
        {
            OnDestroyExt();
            OnDestroyed?.Invoke();
        }

        #endregion


        #region Update, LateUpdate, FixedUpdate

        void IUpdateRunner.AddUpdate(Action action) => AddUpdate(action);

        void IUpdateRunner.RemoveUpdate(Action action) => RemoveUpdate(action);

        void IUpdateRunner.AddLateUpdate(Action action) => AddLateUpdate(action);

        void IUpdateRunner.RemoveLateUpdate(Action action) => RemoveLateUpdate(action);

        void IUpdateRunner.AddFixedUpdate(Action action) => AddFixedUpdate(action);

        void IUpdateRunner.RemoveFixedUpdate(Action action) => RemoveFixedUpdate(action);

        private List<Action> _updates = new List<Action>();
        private List<Action> _lateUpdates = new List<Action>();
        private List<Action> _fixedUpdates = new List<Action>();

        private void Update()
        {
            for (int i = 0; i < _updates.Count; i++)
            {
                _updates[i]?.Invoke();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateUpdates.Count; i++)
            {
                _lateUpdates[i]?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdates.Count; i++)
            {
                _fixedUpdates[i]?.Invoke();
            }
        }

        public void AddUpdate(Action action) => _updates.Add(action);
        public void RemoveUpdate(Action action) => _updates.Remove(action);

        public void AddLateUpdate(Action action) => _lateUpdates.Add(action);
        public void RemoveLateUpdate(Action action) => _lateUpdates.Remove(action);

        public void AddFixedUpdate(Action action) => _fixedUpdates.Add(action);
        public void RemoveFixedUpdate(Action action) => _fixedUpdates.Remove(action);

        protected virtual void UpdateExt() { }

        protected virtual void LateUpdateExt() { }

        protected virtual void FixedUpdateExt() { }


        private void AddUpdates()
        {
            if (UpdateRunner == null) { throw new ArgumentNullException($"_updateRunner"); }

            UpdateRunner.AddUpdate(UpdateExt);
            UpdateRunner.AddLateUpdate(LateUpdateExt);
            UpdateRunner.AddFixedUpdate(FixedUpdateExt);
        }

        private void RemoveUpdates()
        {
            if (UpdateRunner == null) { throw new ArgumentNullException($"_updateRunner"); }

            UpdateRunner.RemoveUpdate(UpdateExt);
            UpdateRunner.RemoveLateUpdate(LateUpdateExt);
            UpdateRunner.RemoveFixedUpdate(FixedUpdateExt);
        }

        #endregion


        #region GetComponent

        public new T[] GetComponents<T>()
        {
            T[] components = base.GetComponents<T>();
            components = ExcludeCallingComponent(components);
            components = TryAwake(components);
            return components;
        }

        public new T[] GetComponentsInParent<T>()
        {
            T[] components = base.GetComponentsInParent<T>();
            components = ExcludeCallingComponent(components);
            components = TryAwake(components);
            return components;
        }

        public new T[] GetComponentsInChildren<T>()
        {
            T[] components = base.GetComponentsInChildren<T>();
            components = ExcludeCallingComponent(components);
            components = TryAwake(components);
            return components;
        }

        public new T GetComponent<T>()
        {
            T[] components = GetComponents<T>();
            return FindSingleComponent(components, "this gameObject");
        }

        public new T GetComponentInChildren<T>()
        {
            T[] components = GetComponentsInChildren<T>();
            return FindSingleComponent(components, "this gameObject or it's children");
        }

        public new T GetComponentInParent<T>()
        {
            T[] components = GetComponentsInParent<T>();
            return FindSingleComponent(components, "this gameObject or it's parents");
        }

        private T[] ExcludeCallingComponent<T>(T[] components)
        {
            return components.Where(it => !ReferenceEquals(it, this)).ToArray();
        }

        private T FindSingleComponent<T>(T[] components, string spaceName)
        {
            if (components.Length == 0)
            {
                throw new NullReferenceException($"Can't find component on {spaceName}!" +
                    $" {GetTypeAndGameObjectNameLog(typeof(T))}");
            }
            if (components.Length > 1)
            {
                string componentsObjectsNames = string.Join("\n",
                    components.Select(it =>
                    {
                        if (it is Component itAsComponent) return itAsComponent.gameObject.name;
                        else return it.GetType().Name;
                    })
                    .ToArray());


                throw new InvalidOperationException($"There is more than one component on {spaceName}!" +
                    $" {GetTypeAndGameObjectNameLog(typeof(T))}.\n{componentsObjectsNames}");
            }
            return components[0];
        }

        private string GetTypeAndGameObjectNameLog(Type type)
        {
            return $"GameObject: {gameObject.name}, Type: {type.Name}";
        }

        #endregion

        private const BindingFlags allObjectBinding = BindingFlags.Instance |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;

        public IUpdateRunner UpdateRunner => this;

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
    }
}
