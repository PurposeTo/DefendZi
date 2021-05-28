using System;
using System.Collections;

namespace Desdiene.Coroutine.CoroutineExecutor
{
    public interface ICoroutine
    {
        IEnumerator Enumerator { get; }
        bool IsExecuting { get; }

        Action OnAlreadyStarted { get; set; }
        Action OnStop { get; set; }
        Action OnIsAlreadyStopped { get; set; }
    }
}
