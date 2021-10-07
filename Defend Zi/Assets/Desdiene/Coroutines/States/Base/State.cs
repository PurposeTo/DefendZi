using System;
using System.Collections;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint<CoroutineWrap>
        {
            private readonly IStateSwitcher<State, CoroutineWrap> _stateSwitcher;
            private readonly CoroutineWrap _it;

            protected State(MonoBehaviourExt mono,
                            IStateSwitcher<State, CoroutineWrap> stateSwitcher,
                            CoroutineWrap it) : base(mono)
            {
                _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
                _it = it ?? throw new ArgumentNullException(nameof(it));
            }


            void IStateEntryExitPoint<CoroutineWrap>.OnEnter(CoroutineWrap it)
            {
                OnEnter(it);
            }

            void IStateEntryExitPoint<CoroutineWrap>.OnExit(CoroutineWrap it)
            {
                OnExit(it);
            }

            protected virtual void OnEnter(CoroutineWrap it) { }

            protected virtual void OnExit(CoroutineWrap it) { }

            /// <summary>
            /// Запустить выполнение корутины, если она не была запущена.
            /// </summary>
            public void StartContinuously(IEnumerator enumerator) => StartContinuously(_it, enumerator);

            /// <summary>
            /// Прервать выполнение корутины.
            /// </summary>
            public void Terminate() => Terminate(_it);

            /// <summary>
            /// Прервать выполнение корутины, если она была запущена.
            /// </summary>
            /// <returns>Была ли корутина запущена?</returns>
            public bool TryTerminate()
            {
                bool isExecuting = _it.IsExecuting; // т.к. состояние может поменяться, кешируем.
                if (isExecuting) Terminate();
                return isExecuting;
            }

            /// <summary>
            /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
            /// </summary>
            /// <param name="newCoroutine">Вложенная корутина.</param>
            /// <returns>Енумератор для ожидания выполнения.</returns>
            public IEnumerator StartNested(IEnumerator newCoroutine) => StartNested(_it, newCoroutine);


            protected abstract void StartContinuously(CoroutineWrap it, IEnumerator enumerator);
            protected abstract void Terminate(CoroutineWrap it);
            protected abstract IEnumerator StartNested(CoroutineWrap it, IEnumerator newCoroutine);

            protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
