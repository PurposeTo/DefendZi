using System;

namespace Desdiene.MonoBehaviourExtension
{
    /// <summary>
    /// Содержит поле MonoBehaviourExt, которое инициализируется конструктором
    /// </summary>
    public abstract class MonoBehaviourExtContainer
    {
        private bool _isDestroyed = false;
        private MonoBehaviourExt _monoBehaviourExt;

        public MonoBehaviourExtContainer(MonoBehaviourExt monoBehaviourExt)
        {
            MonoBehaviourExt = monoBehaviourExt != null
                ? monoBehaviourExt
                : throw new ArgumentNullException(nameof(monoBehaviourExt));

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

            private set => _monoBehaviourExt = value;
        }

        protected virtual void OnDestroy() { }

        /// <summary>
        /// Вызвать для освобождения ресурсов и избавления связи с MonoBehaviourExt для последующей сборки мусора.
        /// </summary>
        protected void Destroy()
        {
            if (_isDestroyed) return;

            UnsubscribeEvents();
            OnDestroy();
            MonoBehaviourExt = null;
            _isDestroyed = true;
        }

        private void SubscribeEvents()
        {
            MonoBehaviourExt.OnDestroyed += Destroy;
        }

        private void UnsubscribeEvents()
        {
            MonoBehaviourExt.OnDestroyed -= Destroy;
        }
    }
}
