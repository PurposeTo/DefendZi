using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.Coroutine
{
    public class CoroutineWrap : MonoBehaviourExtContainer, ICoroutine
    {
        public CoroutineWrap(MonoBehaviourExt mono) : base(mono)
        {
            mono.OnDisabled += Break;
        }

        /// <summary>
        /// Событие о остановке корутины. Вызывается в случае Break-а или по окончанию выполнения.
        /// </summary>
        public event Action OnStopped;
        public bool IsExecuting => coroutine != null;
        private UnityEngine.Coroutine coroutine;

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        public void StartContinuously(IEnumerator enumerator) => StartContinuously(enumerator, null);

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        /// <param name="OnAlreadyStarted">Блок кода, выполняемый в том случае, если корутина уже была запущена.</param>
        public void StartContinuously(IEnumerator enumerator, Action OnAlreadyStarted)
        {
            if (!IsExecuting) Start(enumerator);
            else OnAlreadyStarted?.Invoke();
        }

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        public void ReStart(IEnumerator enumerator)
        {
            Break();
            Start(enumerator);
        }

        /// <summary>
        /// Останавливает корутину, если она выполнялась.
        /// </summary>
        public void Break()
        {
            if (IsExecuting)
            {
                monoBehaviourExt.StopCoroutine(coroutine);
                SetNullAndRemove();
            }
        }

        /// <summary>
        /// Начать выполнение корутины.
        /// </summary>
        private void Start(IEnumerator enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));
            coroutine = monoBehaviourExt.StartCoroutine(WrappedEnumerator(enumerator));
            monoBehaviourExt.AddCoroutine(this);
        }

        private IEnumerator WrappedEnumerator(IEnumerator enumerator)
        {
            yield return enumerator;
            SetNullAndRemove();
        }

        private void SetNullAndRemove()
        {
            SetNull();
            OnStopped?.Invoke();
        }

        private void SetNull() => coroutine = null;

    }
}