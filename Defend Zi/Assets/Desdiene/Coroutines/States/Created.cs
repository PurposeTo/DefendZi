using System;
using System.Collections;
using Desdiene.Coroutines.Components;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private class Created : State
        {
            public Created(MonoBehaviourExt mono,
                           CoroutineWrap it)
                : base(mono,
                       it)
            { }

            protected override void StartContinuously(CoroutineWrap it, IEnumerator enumerator)
            {
                it._coroutinesStack.Clear();
                it._coroutinesStack.Add(enumerator);
                SwitchState<Executing>();
            }

            protected override void Terminate(CoroutineWrap it)
            {
                Debug.LogError("You need to start coroutine, before terminate it");
            }

            protected override IEnumerator StartNested(CoroutineWrap it, IEnumerator newCoroutine)
            {
                Debug.LogError("You need to start coroutine, before start nested");
                yield break;
            }
        }
    }
}
