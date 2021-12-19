using System;

namespace Desdiene.MonoBehaviourExtension
{
    /// <summary>
    /// Содержит поле MonoBehaviourExt, которое инициализируется конструктором
    /// </summary>
    public abstract class MonoBehaviourExtContainer
    {
        private bool _isDestroyed = false;
        private readonly MonoBehaviourExt _monoBehaviourExt;
        private readonly IUpdateRunner _updateRunner;

        public MonoBehaviourExtContainer(MonoBehaviourExt monoBehaviourExt)
        {
            _monoBehaviourExt = monoBehaviourExt != null
                ? monoBehaviourExt
                : throw new ArgumentNullException(nameof(monoBehaviourExt));

            _updateRunner = _monoBehaviourExt;

            SubscribeEvents();
        }

        protected MonoBehaviourExt MonoBehaviourExt
        {
            get
            {
                return _isDestroyed
                    ? throw new InvalidOperationException("Данный объект был помечен как удаленный. Взаимодействие невозможно.")
                    : _monoBehaviourExt;
            }
        }

        protected virtual void OnDestroy() { }

        protected virtual void UpdateExt() { }

        protected virtual void LateUpdateExt() { }

        protected virtual void FixedUpdateExt() { }

        /// <summary>
        /// Вызвать для освобождения ресурсов и избавления связи с MonoBehaviourExt для последующей сборки мусора.
        /// </summary>
        protected void Destroy()
        {
            if (_isDestroyed) return;

            UnsubscribeEvents();
            OnDestroy();
            _isDestroyed = true;
        }

        private void SubscribeEvents()
        {
            MonoBehaviourExt.OnDestroyed += Destroy;
            MonoBehaviourExt.OnEnabled += AddUpdates;
            MonoBehaviourExt.OnDisabled += RemoveUpdates;
        }

        private void UnsubscribeEvents()
        {
            MonoBehaviourExt.OnDestroyed -= Destroy;
            MonoBehaviourExt.OnEnabled -= AddUpdates;
            MonoBehaviourExt.OnDisabled -= RemoveUpdates;
        }

        private void AddUpdates()
        {
            _updateRunner.AddUpdate(UpdateExt);
            _updateRunner.AddLateUpdate(LateUpdateExt);
            _updateRunner.AddFixedUpdate(FixedUpdateExt);
        }

        private void RemoveUpdates()
        {
            _updateRunner.RemoveUpdate(UpdateExt);
            _updateRunner.RemoveLateUpdate(LateUpdateExt);
            _updateRunner.RemoveFixedUpdate(FixedUpdateExt);
        }
    }
}
