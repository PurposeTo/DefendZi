using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes.States;
using Desdiene.Types.Processes.States.Base;

namespace Desdiene.Types.Processes
{
    public class Process : IProcess
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private readonly string _name;

        public Process(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _name = name;
            StateSwitcher<State, StateContext> stateSwitcher = new StateSwitcher<State, StateContext>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(stateSwitcher, _name),
                new Running(stateSwitcher, _name),
                new Completed(stateSwitcher, _name)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();
        }

        event Action IProcessNotifier.OnStarted
        {
            add => CurrentState.OnStarted += value;
            remove => CurrentState.OnStarted -= value;
        }

        event Action IProcessNotifier.OnCompleted
        {
            add => CurrentState.OnCompleted += value;
            remove => CurrentState.OnCompleted -= value;
        }

        event Action<IProcessGetter> IProcessNotifier.OnChanged
        {
            add => CurrentState.OnChanged += () => value?.Invoke(this);
            remove => CurrentState.OnChanged -= () => value?.Invoke(this);
        }

        string IProcessGetter.Name => _name;
        bool IProcessGetter.KeepWaiting => CurrentState.KeepWaiting;

        IProcess IProcessSetter.Start()
        {
            CurrentState.Start();
            return this;
        }

        IProcess IProcessSetter.Complete()
        {
            CurrentState.Complete();
            return this;
        }

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));
    }
}
