using System.Collections;
using Desdiene.MonoBehaviourExtension;
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

            protected override void OnEnter()
            {
                It._isExecuting.Set(false);
            }

            public override void StartContinuously(IEnumerator enumerator)
            {
                It._coroutinesStack.Add(enumerator);
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
}
