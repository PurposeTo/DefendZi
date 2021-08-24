using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Desdiene.Container;
using Desdiene.CoroutineWrapper.States;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.Types.AtomicReference;
using UnityEngine;

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
    public class StoppableCoroutine : MonoBehaviourExtContainer
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public StoppableCoroutine(MonoBehaviourExt mono, IEnumerator initialCoroutine) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (initialCoroutine is null) throw new ArgumentNullException(nameof(initialCoroutine));

            NestableCoroutine nestableCoroutine = new NestableCoroutine(initialCoroutine);
            StateSwitcher<State, MutableData> stateSwitcher = new StateSwitcher<State, MutableData>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(mono, stateSwitcher, nestableCoroutine),
                new Executing(mono, stateSwitcher, nestableCoroutine),
                new Executed(mono, stateSwitcher, nestableCoroutine),
                new Terminated(mono, stateSwitcher, nestableCoroutine),
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();
        }

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        public void StartContinuously() => CurrentState.StartContinuously();

        /// <summary>
        /// Прервать выполнение корутины.
        /// </summary>
        public void Terminate() => CurrentState.Terminate();

        /// <summary>
        /// Прервать выполнение корутины, если она была запущена.
        /// </summary>
        /// <returns>Была ли корутина запущена?</returns>
        public bool TryTerminate() => CurrentState.TryTerminate();

        /// <summary>
        /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
        /// </summary>
        /// <param name="newCoroutine">Вложенная корутина.</param>
        /// <returns>Енумератор для ожидания выполнения.</returns>
        public IEnumerator StartNested(IEnumerator newCoroutine) => CurrentState.StartNested(newCoroutine);
    }
}
