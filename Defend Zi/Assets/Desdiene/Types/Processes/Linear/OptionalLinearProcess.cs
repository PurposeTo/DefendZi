using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.Types.Processes
{

    /// <summary>
    /// Опциональный линейный процесс: после выключения не может быть включен. 
    /// Не обязательно ждать, если он не был запущен на момент обращения. 
    /// </summary>
    public partial class OptionalLinearProcess : IProcess
    {
        private readonly IStateSwitcher<State> _stateSwitcher;
        private readonly string _name;

        public OptionalLinearProcess(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _name = name;

            State initState = new CreatedNonWaiting(this);
            List<State> allStates = new List<State>()
            {
                initState,
                new Running(this),
                new Completed(this)
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);
        }

        private event Action<IProcessAccessor> OnChanged;
        private event Action WhenRunning;
        private event Action WhenCompleted;

        event Action IProcessNotifier.WhenRunning
        {
            add
            {
                WhenRunning = CurrentState.SubscribeToWhenRunning(WhenRunning, value);
            }
            remove => WhenRunning -= value;
        }

        event Action IProcessNotifier.WhenCompleted
        {
            add
            {
                WhenCompleted = CurrentState.SubscribeToWhenCompleted(WhenCompleted, value);
            }
            remove => WhenCompleted -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        string IProcessAccessor.Name => _name;
        bool IProcessAccessor.KeepWaiting => CurrentState.KeepWaiting;

        void IProcessMutator.Start() => CurrentState.Start();

        void IProcessMutator.Stop() => CurrentState.Complete();

        private State CurrentState => _stateSwitcher.CurrentState;

        private State SwitchState<stateT>() where stateT : State
        {
            bool pastKeepWaiting = CurrentState.KeepWaiting;
            State nextState = _stateSwitcher.Switch<stateT>();
            bool nextKeepWaiting = nextState.KeepWaiting;
            if (pastKeepWaiting != nextKeepWaiting) OnChanged?.Invoke(this);
            return nextState;
        }
    }
}
