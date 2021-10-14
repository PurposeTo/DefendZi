using System;
using System.Collections;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private class Executed : State
        {
            public Executed(MonoBehaviourExt mono,
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
                It.WhenCompleted?.Invoke();
            }

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
}
