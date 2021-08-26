using System;
using System.Collections;
using Desdiene.CoroutineWrapper.Components;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States
{
    public class Created : State
    {
        public Created(MonoBehaviourExt mono,
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
            CoroutinesStack.Clear();
            CoroutinesStack.Add(enumerator);
            SwitchState<Executing>();
        }

        public override void Terminate()
        {
            Debug.LogError("You need to start coroutine, before terminate it");
        }

        public override IEnumerator StartNested(IEnumerator newCoroutine)
        {
            Debug.LogError("You need to start coroutine, before start nested");
            yield break;
        }
    }
}
