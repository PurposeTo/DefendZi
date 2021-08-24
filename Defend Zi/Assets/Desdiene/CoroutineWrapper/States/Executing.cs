using System.Collections;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;

namespace Desdiene.CoroutineWrapper.States
{
    public class Executing : State
    {
        public Executing(MonoBehaviourExt mono,
                       IStateSwitcher<State, MutableData> stateSwitcher,
                         NestableCoroutine nestableCoroutine)
            : base(mono,
                   stateSwitcher,
                   nestableCoroutine)
        { }

        protected override void OnEnter()
        {
            Coroutine = monoBehaviourExt.StartCoroutine(Run());
        }

        public override void StartContinuously()
        {
            throw new System.NotImplementedException();
        }

        public override void Terminate()
        {
            SwitchState<Terminated>();
        }

        public override IEnumerator StartNested(IEnumerator newCoroutine)
        {
            NestableCoroutine.Add(newCoroutine);
            while (NestableCoroutine.IsCoroutineContains(newCoroutine) == false)
            {
                yield return null;
            }
        }

        private IEnumerator Run()
        {
            while (IsExecuting && NestableCoroutine.MoveNext())
            {
                yield return NestableCoroutine.Current;
            }
            SwitchState<Executed>();
        }
    }
}
