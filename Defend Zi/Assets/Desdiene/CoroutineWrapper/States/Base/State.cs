using System.Collections;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint<MutableData>
    {
        private readonly IStateSwitcher<State, MutableData> _stateSwitcher;

        public State(MonoBehaviourExt mono,
                     IStateSwitcher<State, MutableData> stateSwitcher,
                     NestableCoroutine nestableCoroutine) : base(mono)
        {
            _stateSwitcher = stateSwitcher;
            NestableCoroutine = nestableCoroutine;
        }

        public bool IsExecuting { get; private set; }

        protected NestableCoroutine NestableCoroutine { get; }
        protected Coroutine Coroutine { get; set; }

        void IStateEntryExitPoint<MutableData>.OnEnter(MutableData mutableData)
        {
            IsExecuting = this is Executing;

            if (mutableData != null)
            {
                Coroutine = mutableData.Coroutine;
            }

            OnEnter();
        }

        MutableData IStateEntryExitPoint<MutableData>.OnExit()
        {
            OnExit();
            return new MutableData(Coroutine);
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        /// <summary>
        /// Запустить выполнение корутины, если она не была запущена.
        /// </summary>
        public abstract void StartContinuously();

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

        protected void SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
