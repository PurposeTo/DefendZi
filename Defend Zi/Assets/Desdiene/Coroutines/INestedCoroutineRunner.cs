using System.Collections;

namespace Desdiene.Coroutines
{
    public interface INestedCoroutineRunner
    {
        /// <summary>
        /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
        /// </summary>
        /// <param name="newCoroutine">Вложенная корутина.</param>
        /// <returns>Енумератор для ожидания выполнения.</returns>
        public IEnumerator StartNested(IEnumerator newCoroutine);
    }
}
