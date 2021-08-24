using System.Collections;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.State;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint<DynamicData>
    {
        private readonly IStateSwitcher<State, DynamicData> _stateSwitcher;

        public State(MonoBehaviourExt mono,
                     IStateSwitcher<State, DynamicData> stateSwitcher,
                     NestableCoroutine nestableCoroutine) : base(mono)
        {
            _stateSwitcher = stateSwitcher;
            NestableCoroutine = nestableCoroutine;
        }

        public bool IsExecuting { get; private set; }

        protected NestableCoroutine NestableCoroutine { get; }
        protected Coroutine Coroutine { get; set; }

        void IStateEntryExitPoint<DynamicData>.OnEnter(DynamicData dynamicData)
        {
            IsExecuting = GetType() == typeof(Executing);

            if (dynamicData != null)
            {
                Coroutine = dynamicData.Coroutine;
            }

            OnEnter();
        }

        DynamicData IStateEntryExitPoint<DynamicData>.OnExit()
        {
            OnExit();
            return new DynamicData(Coroutine);
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        public abstract void StartContinuously();
        public abstract void Terminate();
        public abstract IEnumerator StartNested(IEnumerator newCoroutine);

        protected void SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
