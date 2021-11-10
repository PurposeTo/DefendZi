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

            protected override void OnEnter()
            {
                It._isExecuting.Set(true);
                It.OnStarted?.Invoke();
                It.WhenRunning?.Invoke();
                It._coroutine = MonoBehaviourExt.StartCoroutine(Run());
            }

            protected override void OnExit()
            {
                It._isExecuting.Set(false);
            }

            public override void StartContinuously(IEnumerator enumerator)
            {
                Debug.LogWarning("You can't start coroutine, because it is executing now");
            }

            public override void Terminate()
            {
                SwitchState<Terminated>();
            }

            /// <summary>
            /// Использование с "yield return" - запустить и дождаться выполнения корутины.
            /// Если не использовать с "yield return", то корутина не будет запущена.
            /// </summary>
            public override IEnumerator StartNested(IEnumerator newCoroutine)
            {
                It._coroutinesStack.Add(newCoroutine);
                yield break;
            }

            private IEnumerator Run()
            {
                while (It.IsExecuting && It._coroutinesStack.MoveNext())
                {
                    yield return It._coroutinesStack.Current;
                }

                SwitchState<Executed>();
            }
        }
    }
}