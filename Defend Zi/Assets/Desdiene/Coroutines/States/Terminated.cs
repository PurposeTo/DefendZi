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

            protected override void OnEnter()
            {
                if (It._coroutine != null)
                {
                    MonoBehaviourExt.StopCoroutine(It._coroutine);
                    It._coroutine = null;
                }
                It.OnStopped?.Invoke();
                It.WhenCompleted?.Invoke();
            }

            public override void StartContinuously(IEnumerator enumerator)
            {
                SwitchState<Created>().StartContinuously(enumerator);
            }

            public override void Terminate()
            {
                Debug.LogError("You can't terminate coroutine, because it is already terminated");
            }

            public override IEnumerator StartNested(IEnumerator newCoroutine)
            {
                Debug.LogError("You can't start nested coroutine, because coroutine is terminated");
                yield break;
            }
        }
    }
}
