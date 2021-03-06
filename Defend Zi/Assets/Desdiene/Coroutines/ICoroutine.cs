using System;
using System.Collections;
using Desdiene.Types.Processes;

namespace Desdiene.Coroutines
{
    public interface ICoroutine : INestedCoroutineRunner, IProcessAccessorNotifier
    {
        public event Action OnStarted;
        public event Action OnStopped;

        bool IsExecuting { get; }

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        public void StartContinuously(IEnumerator enumerator);

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        void ReStart(IEnumerator enumerator);

        /// <summary>
        /// Прервать выполнение корутины.
        /// </summary>
        public void Terminate();

        /// <summary>
        /// Прервать выполнение корутины, если она была запущена.
        /// </summary>
        /// <returns>Была ли корутина запущена?</returns>
        public bool TryTerminate();
    }
}
