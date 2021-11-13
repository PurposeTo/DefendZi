using System;
using System.Collections;
using Desdiene.MonoBehaviourExtension;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.States;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint
        {
            protected State(MonoBehaviourExt mono,
                            CoroutineWrap it) : base(mono)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
            }

            void IStateEntryExitPoint.OnEnter()
            {
                OnEnter();
            }

            void IStateEntryExitPoint.OnExit()
            {
                OnExit();
            }

            protected CoroutineWrap It { get; }

            public virtual Action SubscribeToWhenRunning(Action action, Action value) => action += value;
            public virtual Action SubscribeToWhenCompleted(Action action, Action value) => action += value;

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
                bool isExecuting = It.IsExecuting; // т.к. состояние может поменяться, кешируем.
                if (isExecuting) Terminate();
                return isExecuting;
            }

            /// <summary>
            /// Запустить выполнение вложенной корутины (аналогия со вложенными методами).
            /// </summary>
            /// <param name="newCoroutine">Вложенная корутина.</param>
            /// <returns>Енумератор для ожидания выполнения.</returns>
            public abstract IEnumerator StartNested(IEnumerator newCoroutine);

            protected virtual void OnEnter() { }

            protected virtual void OnExit() { }

            protected State SwitchState<stateT>() where stateT : State => It.SwitchState<stateT>();
        }
    }
}
