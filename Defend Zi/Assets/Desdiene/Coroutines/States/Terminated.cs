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
        private class Terminated : State
        {
            public Terminated(MonoBehaviourExt mono,
                           IStateSwitcher<State, CoroutineWrap> stateSwitcher,
                           CoroutineWrap it)
                : base(mono,
                       stateSwitcher,
                       it)
            { }

            protected override void OnEnter(CoroutineWrap it)
            {
                if (it._coroutine != null)
                {
                    MonoBehaviourExt.StopCoroutine(it._coroutine);
                    it._coroutine = null;
                }
            }

            protected override void StartContinuously(CoroutineWrap it, IEnumerator enumerator)
            {
                SwitchState<Created>().StartContinuously(enumerator);
            }

            protected override void Terminate(CoroutineWrap it)
            {
                Debug.LogError("You can't terminate coroutine, because it is already terminated");
            }

            protected override IEnumerator StartNested(CoroutineWrap it, IEnumerator newCoroutine)
            {
                Debug.LogError("You can't start nested coroutine, because coroutine is terminated");
                yield break;
            }
        }
    }
}
