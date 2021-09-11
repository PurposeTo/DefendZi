using System;
using System.Collections;
using Desdiene.Coroutines.Components;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.Coroutines.States
{
    public class Executed : State
    {
        public Executed(MonoBehaviourExt mono,
                       IStateSwitcher<State> stateSwitcher,
                       StateContext stateContext,
                       CoroutinesStack coroutinesStack,
                       Func<bool> isExecutingRef)
            : base(mono,
                   stateSwitcher,
                   stateContext,
                   coroutinesStack,
                   isExecutingRef)
        { }

        public override void StartContinuously(IEnumerator enumerator)
        {
            SwitchState<Created>().StartContinuously(enumerator);
        }

        public override void Terminate()
        {
            Debug.LogError("You can't terminate coroutine, because it is executed");
        }

        public override IEnumerator StartNested(IEnumerator newCoroutine)
        {
            Debug.LogError("You can't start nested coroutine, because coroutine is executed");
            yield break;
        }
    }
}
