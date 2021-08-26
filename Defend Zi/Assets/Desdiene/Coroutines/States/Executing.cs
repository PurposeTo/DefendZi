using System;
using System.Collections;
using Desdiene.Coroutines.Components;
using Desdiene.Coroutines.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.Coroutines.States
{
    public class Executing : State
    {
        public Executing(MonoBehaviourExt mono,
                        IStateSwitcher<State, MutableData> stateSwitcher,
                        CoroutinesStack coroutinesStack,
                        Func<bool> isExecutingRef)
            : base(mono,
                   stateSwitcher,
                   coroutinesStack,
                   isExecutingRef)
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
