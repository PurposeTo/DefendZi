using System;
using System.Collections;

namespace Desdiene.Coroutine
{
    public interface ICoroutine
    {
        bool IsExecuting { get; }

        event Action OnStopped;

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        void StartContinuously(IEnumerator enumerator);

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        /// <param name="OnAlreadyStarted">Блок кода, выполняемый в том случае, если корутина уже была запущена.</param>
        void StartContinuously(IEnumerator enumerator, Action OnAlreadyStarted);

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        void ReStart(IEnumerator enumerator);

        /// <summary>
        /// Останавливает корутину, если она выполнялась.
        /// </summary>
        void Break();
    }
}
