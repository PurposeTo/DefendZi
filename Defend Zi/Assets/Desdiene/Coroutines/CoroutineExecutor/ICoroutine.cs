using System;
using System.Collections;

namespace Desdiene.Coroutine.CoroutineExecutor
{
    public interface ICoroutine
    {
        bool IsExecuting { get; }

        Action OnAlreadyStarted { get; set; }
        Action OnStop { get; set; }
        Action OnIsAlreadyStopped { get; set; }

        ///// <summary>
        ///// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        ///// </summary>
        ///// <param name="enumerator">IEnumerator для выполнения</param>
        //void ExecuteCoroutineContinuously(IEnumerator enumerator);

        ///// <summary>
        ///// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        ///// </summary>
        ///// <param name="enumerator">IEnumerator для выполнения</param>
        //void ReStartExecution(IEnumerator enumerator);

        ///// <summary>
        ///// Останавливает корутину, если она выполнялась.
        ///// </summary>
        //void BreakCoroutine();
    }
}
