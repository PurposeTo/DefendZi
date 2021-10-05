using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.Containers;
using Desdiene.Coroutines.Components;
using Desdiene.Coroutines.States;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;

namespace Desdiene.Coroutines
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
    public sealed class CoroutineWrap : MonoBehaviourExtContainer, ICoroutine
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public CoroutineWrap(MonoBehaviourExt mono) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            StateContext stateContext = new StateContext();
            CoroutinesStack coroutineStack = new CoroutinesStack();
            Func<bool> isExecutingRef = () => CurrentState is Executing;
            StateSwitcher<State> stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(mono, stateSwitcher, stateContext, coroutineStack, isExecutingRef),
                new Executing(mono, stateSwitcher, stateContext, coroutineStack, isExecutingRef),
                new Executed(mono, stateSwitcher, stateContext, coroutineStack, isExecutingRef),
                new Terminated(mono, stateSwitcher, stateContext, coroutineStack, isExecutingRef)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();
        }

        public bool IsExecuting => CurrentState.IsExecuting;
        private State CurrentState => _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        void ICoroutine.StartContinuously(IEnumerator enumerator) => CurrentState.StartContinuously(enumerator);

        /// <summary>
        /// Если корутина была запущена, остановить её. Запустить выполнение корутины.
        /// </summary>
        void ICoroutine.ReStart(IEnumerator enumerator)
        {
            CurrentState.TryTerminate();
            CurrentState.StartContinuously(enumerator);
        }

        /// <summary>
        /// Прервать выполнение корутины.
        /// </summary>
        void ICoroutine.Terminate() => CurrentState.Terminate();

        /// <summary>
        /// Прервать выполнение корутины, если она была запущена.
        /// </summary>
        /// <returns>Была ли корутина запущена?</returns>
        bool ICoroutine.TryTerminate() => CurrentState.TryTerminate();

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
        IEnumerator ICoroutine.StartNested(IEnumerator newCoroutine) => CurrentState.StartNested(newCoroutine);

        protected override void OnDestroy()
        {
            CurrentState.TryTerminate();
        }
    }
}
