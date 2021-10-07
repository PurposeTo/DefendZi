using System;
using System.Collections;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.Coroutines
{
    public partial class CoroutineWrap
    {
        private class Executing : State
        {
            public Executing(MonoBehaviourExt mono,
                             CoroutineWrap it)
                : base(mono,
                       it)
            { }

            public override Action SubscribeToWhenRunning(Action action, Action value)
            {
                value?.Invoke();
                return base.SubscribeToWhenRunning(action, value);
            }

            protected override void OnEnter(CoroutineWrap it)
            {
                it.WhenRunning?.Invoke();
                it._coroutine = MonoBehaviourExt.StartCoroutine(Run(it));
            }

            protected override void StartContinuously(CoroutineWrap it, IEnumerator enumerator)
            {
                Debug.LogWarning("You can't start coroutine, because it is executing now");
            }

            protected override void Terminate(CoroutineWrap it)
            {
                SwitchState<Terminated>();
            }

            /// <summary>
            /// Использование с "yield return" - запустить и дождаться выполнения корутины.
            /// Если не использовать с "yield return", то корутина не будет запущена.
            /// </summary>
            protected override IEnumerator StartNested(CoroutineWrap it, IEnumerator newCoroutine)
            {
                it._coroutinesStack.Add(newCoroutine);
                yield break;
            }

            private IEnumerator Run(CoroutineWrap it)
            {
                while (it.IsExecuting && it._coroutinesStack.MoveNext())
                {
                    yield return it._coroutinesStack.Current;
                }

                SwitchState<Executed>();
            }
        }
    }
}