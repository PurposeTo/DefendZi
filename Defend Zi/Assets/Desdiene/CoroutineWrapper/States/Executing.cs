using System.Collections;
using Desdiene.CoroutineWrapper.Components;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States
{
    public class Executing : State
    {
        public Executing(MonoBehaviourExt mono,
                       IStateSwitcher<State, MutableData> stateSwitcher,
                         CoroutinesStack coroutinesStack)
            : base(mono,
                   stateSwitcher,
                   coroutinesStack)
        { }

        protected override void OnEnter()
        {
            Coroutine = monoBehaviourExt.StartCoroutine(Run());
        }

        public override void StartContinuously(IEnumerator enumerator)
        {
            Debug.LogWarning("You can't start coroutine, because it is executing now");
        }

        public override void Terminate()
        {
            SwitchState<Terminated>();
        }

        /// <summary>
        /// Использование с "yield return" - запустить и дождаться выполнения корутины.
        /// Если не использовать с "yield return", то корутина не будет запущена.
        /// </summary>
        public override IEnumerator StartNested(IEnumerator newCoroutine)
        {
            CoroutinesStack.Add(newCoroutine);
            yield break;
        }

        private IEnumerator Run()
        {
            while (IsExecuting && CoroutinesStack.MoveNext())
            {
                yield return CoroutinesStack.Current;
            }
            SwitchState<Executed>();
        }
    }
}
