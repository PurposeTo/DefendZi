using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Desdiene.Container;
using Desdiene.CoroutineWrapper.States;
using Desdiene.CoroutineWrapper.States.Base;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.Types.AtomicReference;
using UnityEngine;

namespace Desdiene.CoroutineWrapper
{
    /*
     * ��� ��������� �������� ����������� ���������� �� ���������� yield return. 
     * ���� �������� ��������������� ������ yield return _innerEnumerator, �� ������ ��� ����������� �� �����.
     * ������ ������� ������:
     * 
     * [_routine = ���������� �������� TestProcess()]
     * 
     * IEnumerator TestProcess() 
     * {
     *     while (true)
     *     {
     *         yield return null;
     *         _routine.Stop();
     *         Debug.Log("���");
     *     }
     * }
     *    
     * ��� ����� �������, �.�. yield return ����� ���� � ��������� �������� ����� while.
     */
    public class StoppableCoroutine : MonoBehaviourExtContainer
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();

        public StoppableCoroutine(MonoBehaviourExt mono, IEnumerator initialCoroutine) : base(mono)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (initialCoroutine is null) throw new ArgumentNullException(nameof(initialCoroutine));

            NestableCoroutine nestableCoroutine = new NestableCoroutine(initialCoroutine);
            StateSwitcher<State, DynamicData> stateSwitcher = new StateSwitcher<State, DynamicData>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(mono, stateSwitcher, nestableCoroutine),
                new Executing(mono, stateSwitcher, nestableCoroutine),
                new Executed(mono, stateSwitcher, nestableCoroutine),
                new Terminated(mono, stateSwitcher, nestableCoroutine),
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();
        }

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        public void StartContinuously() => CurrentState.StartContinuously();

        public void Terminate() => CurrentState.Terminate();

        public IEnumerator StartNested(IEnumerator newCoroutine) => CurrentState.StartNested(newCoroutine);
    }
}
