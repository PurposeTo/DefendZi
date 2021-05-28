using System;
using System.Collections;

namespace Desdiene.Coroutine.CoroutineExecutor
{
    public sealed class CoroutineWithData : ICoroutine
    {
        public IEnumerator Enumerator { get; private set; } = null;
        public UnityEngine.Coroutine Coroutine { get; private set; } = null;
        public bool IsExecuting => Coroutine != null;


        public void SetCoroutine(UnityEngine.Coroutine coroutine)
        {
            Coroutine = coroutine ?? throw new ArgumentNullException(nameof(coroutine));
        }


        public void SetEnumerator(IEnumerator enumerator)
        {
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }


        /// <summary>
        /// Выполняется во время выполнении метода ExecuteCoroutineContinuously, 
        /// в случае, если корутина уже была запущена.
        /// </summary>
        public Action OnAlreadyStarted { get; set; } = null;
        /// <summary>
        /// Выполняется во время выполнении метода BreakCoroutine,
        /// после остановки корутины.
        /// </summary>
        public Action OnStop { get; set; } = null;
        /// <summary>
        /// Выполняется во время выполнении метода BreakCoroutine,
        /// в случае, если корутина УЖЕ была остановлена.
        /// </summary>
        public Action OnIsAlreadyStopped { get; set; } = null;


        public void SetNullToCoroutine() => Coroutine = null;
    }

}
