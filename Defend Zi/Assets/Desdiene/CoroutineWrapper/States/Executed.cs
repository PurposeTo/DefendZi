using System.Collections;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;

namespace Desdiene.CoroutineWrapper.States
{
    public class Executed : State
    {
        public Executed(MonoBehaviourExt mono,
                       IStateSwitcher<State, DynamicData> stateSwitcher,
                       NestableCoroutine nestableCoroutine)
            : base(mono,
                   stateSwitcher,
                   nestableCoroutine)
        { }

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
