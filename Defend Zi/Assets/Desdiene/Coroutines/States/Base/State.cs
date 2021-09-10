using System;
using System.Collections;
using Desdiene.Containers;
using Desdiene.Coroutines.Components;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.Coroutines.States
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly StateContext _stateContext;
        private readonly Func<bool> _isExecutingRef;

        public State(MonoBehaviourExt mono,
                     IStateSwitcher<State> stateSwitcher,
                     StateContext stateContext,
                     CoroutinesStack coroutinesStack,
                     Func<bool> isExecutingRef) : base(mono)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _stateContext = stateContext ?? throw new ArgumentNullException(nameof(stateContext));
            CoroutinesStack = coroutinesStack ?? throw new ArgumentNullException(nameof(coroutinesStack));
            _isExecutingRef = isExecutingRef ?? throw new ArgumentNullException(nameof(isExecutingRef));
        }

        public bool IsExecuting => _isExecutingRef.Invoke();

        protected CoroutinesStack CoroutinesStack { get; }
        protected Coroutine Coroutine { get => _stateContext.Coroutine; set => _stateContext.Coroutine = value; }

        void IStateEntryExitPoint.OnEnter()
        {
            OnEnter();
        }

        void IStateEntryExitPoint.OnExit()
        {
            OnExit();
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        public abstract void StartContinuously(IEnumerator enumerator);

        /// <summary>
        /// Прервать выполнение корутины.
        /// </summary>
        public abstract void Terminate();

        /// <summary>
        /// Прервать выполнение корутины, если она была запущена.
        /// </summary>
        /// <returns>Была ли корутина запущена?</returns>
        public bool TryTerminate()
        {
            bool isExecuting = IsExecuting; // т.к. состояние может поменяться, кешируем.
            if (isExecuting) Terminate();
            return isExecuting;
        }

        /// <summary>
        /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
        /// </summary>
        /// <param name="newCoroutine">Вложенная корутина.</param>
        /// <returns>Енумератор для ожидания выполнения.</returns>
        public abstract IEnumerator StartNested(IEnumerator newCoroutine);

        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
