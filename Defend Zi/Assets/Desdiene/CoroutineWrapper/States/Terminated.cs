using System.Collections;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;

namespace Desdiene.CoroutineWrapper.States
{
    public class Terminated : State
    {
        public Terminated(MonoBehaviourExt mono,
                       IStateSwitcher<State, DynamicData> stateSwitcher,
                       NestableCoroutine nestableCoroutine)
            : base(mono,
                   stateSwitcher,
                   nestableCoroutine)
        { }

        protected override void OnEnter()
        {
            monoBehaviourExt.StopCoroutine(Coroutine);
            Coroutine = null;
        }

        public override void StartContinuously()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator StartNested(IEnumerator newCoroutine)
        {
            throw new System.NotImplementedException();
        }

        public override void Terminate()
        {
            throw new System.NotImplementedException();
        }
    }
}
