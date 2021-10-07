using System;
using System.Collections;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private class Terminated : State
        {
            public Terminated(MonoBehaviourExt mono,
                           CoroutineWrap it)
                : base(mono,
                       it)
            { }

            public override Action SubscribeToWhenCompleted(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenCompleted(action, value);
            }

            protected override void OnEnter(CoroutineWrap it)
            {
                if (it._coroutine != null)
                {
                    MonoBehaviourExt.StopCoroutine(it._coroutine);
                    it._coroutine = null;
                }
                it.WhenCompleted?.Invoke();
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
