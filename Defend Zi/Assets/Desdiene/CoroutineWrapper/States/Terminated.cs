using System.Collections;
using Desdiene.CoroutineWrapper.Components;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using UnityEngine;

namespace Desdiene.CoroutineWrapper.States
{
    public class Terminated : State
    {
        public Terminated(MonoBehaviourExt mono,
                       IStateSwitcher<State, MutableData> stateSwitcher,
                       CoroutinesStack coroutinesStack)
            : base(mono,
                   stateSwitcher,
                   coroutinesStack)
        { }

        protected override void OnEnter()
        {
            monoBehaviourExt.StopCoroutine(Coroutine);
            Coroutine = null;
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
