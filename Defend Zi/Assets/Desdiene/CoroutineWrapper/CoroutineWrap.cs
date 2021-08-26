using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.Container;
using Desdiene.CoroutineWrapper.Components;
using Desdiene.CoroutineWrapper.States;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.Types.AtomicReference;

namespace Desdiene.CoroutineWrapper
{
    /*
     * При остановке корутины выполняться инструкции до следующего yield return. 
     * Если корутина останавливается внутри yield return _innerEnumerator, то дальше она выполняться не будет.
     * Пример неявной работы:
     * 
     * [_routine = запущенная корутина TestProcess()]
     * 
     * IEnumerator TestProcess() 
     * {
     *     while (true)
     *     {
     *         yield return null;
     *         _routine.Stop();
     *         Debug.Log("КРЯ");
     *     }
     * }
     *    
     * Лог будет выведен, т.к. yield return будет лишь в следующей итерации цикла while.
     */
    public class CoroutineWrap : MonoBehaviourExtContainer, ICoroutine
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public CoroutineWrap(MonoBehaviourExt mono) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            Func<bool> isExecutingRef = () => CurrentState is Executing;
            CoroutinesStack coroutineStack = new CoroutinesStack();
            StateSwitcher<State, MutableData> stateSwitcher = new StateSwitcher<State, MutableData>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(mono, stateSwitcher, coroutineStack, isExecutingRef),
                new Executing(mono, stateSwitcher, coroutineStack, isExecutingRef),
                new Executed(mono, stateSwitcher, coroutineStack, isExecutingRef),
                new Terminated(mono, stateSwitcher, coroutineStack, isExecutingRef),
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();
        }

        public bool IsExecuting => CurrentState.IsExecuting;
        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        public void StartContinuously(IEnumerator enumerator) => CurrentState.StartContinuously(enumerator);

        /// <summary>
        /// Если корутина была запущена, остановить её. Запустить выполнение корутины.
        /// </summary>
        public void ReStart(IEnumerator enumerator)
        {
            TryTerminate();
            StartContinuously(enumerator);
        }

        /// <summary>
        /// Прервать выполнение корутины.
        /// </summary>
        public void Terminate() => CurrentState.Terminate();

        /// <summary>
        /// Прервать выполнение корутины, если она была запущена.
        /// </summary>
        /// <returns>Была ли корутина запущена?</returns>
        public bool TryTerminate() => CurrentState.TryTerminate();

        /*
         * метод monoBehaviour.StopCoroutine не может остановить выполнение вложенных корутин, 
         * поэтому все вложенные корутины должны запускаться через yield return StartNested(enumerator).
         * Под капотом реализации идёт ручное перелистывание MoveNext() IEnumerator-а, для возможности отслеживания
         * прерывания корутины и последующего прерывания всех запущенных (вложенных) enumerator-ов.
         */
        /// <summary>
        /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
        /// </summary>
        /// <param name="newCoroutine">Вложенная корутина.</param>
        /// <returns>Енумератор для ожидания выполнения.</returns>
        public IEnumerator StartNested(IEnumerator newCoroutine) => CurrentState.StartNested(newCoroutine);
    }
}
