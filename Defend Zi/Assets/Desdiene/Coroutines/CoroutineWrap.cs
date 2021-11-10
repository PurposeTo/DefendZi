using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.Containers;
using Desdiene.Coroutines.Components;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes;
using UnityEngine;

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

    /// <summary>
    /// Класс для работы с корутиной.
    /// Корутина автоматически останавливается при разрушении монобех объекта, через который она запущена.
    /// </summary>
    public sealed partial class CoroutineWrap : MonoBehaviourExtContainer, ICoroutine
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly CoroutinesStack _coroutinesStack = new CoroutinesStack();
        private readonly IRef<bool> _isExecuting = new Ref<bool>(false);
        private Coroutine _coroutine;

        public CoroutineWrap(MonoBehaviourExt mono) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            State initState = new Created(mono, this);
            List<State> allStates = new List<State>()
            {
                initState,
                new Executing(mono, this),
                new Executed(mono, this),
                new Terminated(mono, this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);

            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            CurrentState.TryTerminate();
            UnsubscribeEvents();
        }

        private event Action OnStarted;
        private event Action OnStopped;

        private event Action WhenRunning;
        private event Action WhenCompleted;
        private event Action<IProcessAccessor> OnChanged;

        event Action IProcessNotifier.WhenRunning
        {
            add
            {
                WhenRunning = CurrentState.SubscribeToWhenRunning(WhenRunning, value);
            }
            remove => WhenRunning -= value;
        }

        event Action IProcessNotifier.WhenCompleted
        {
            add
            {
                WhenCompleted = CurrentState.SubscribeToWhenCompleted(WhenCompleted, value);
            }
            remove => WhenCompleted -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        event Action ICoroutine.OnStarted
        {
            add => OnStarted += value;
            remove => OnStarted -= value;
        }

        event Action ICoroutine.OnStopped
        {
            add => OnStopped += value;
            remove => OnStopped -= value;
        }

        string IProcessAccessor.Name => "Выполнение сопрограммы";

        bool IProcessAccessor.KeepWaiting => IsExecuting;

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
        IEnumerator INestedCoroutineRunner.StartNested(IEnumerator newCoroutine) => CurrentState.StartNested(newCoroutine);

        public bool IsExecuting => _isExecuting.Value;
        private State CurrentState => _stateSwitcher.CurrentState;

        private State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();

        private void SubscribeEvents()
        {
            _isExecuting.OnChanged += InvokeOnChangedProcess;
        }

        private void UnsubscribeEvents()
        {
            _isExecuting.OnChanged -= InvokeOnChangedProcess;
        }

        private void InvokeOnChangedProcess() => OnChanged?.Invoke(this);
    }
}
