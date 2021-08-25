using System.Collections;

namespace Desdiene.CoroutineWrapper
{
    public interface ICoroutine
    {
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

        /// <summary>
        /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
        /// </summary>
        /// <param name="newCoroutine">Вложенная корутина.</param>
        /// <returns>Енумератор для ожидания выполнения.</returns>
        public IEnumerator StartNested(IEnumerator newCoroutine);
    }
}
