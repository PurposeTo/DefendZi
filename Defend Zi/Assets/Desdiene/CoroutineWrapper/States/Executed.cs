using System;
using System.Collections;
using Desdiene.CoroutineWrapper.Components;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States
{
    public class Executed : State
    {
        public Executed(MonoBehaviourExt mono,
                        IStateSwitcher<State, MutableData> stateSwitcher,
                        CoroutinesStack coroutinesStack,
                        Func<bool> isExecutingRef)
            : base(mono,
                   stateSwitcher,
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
