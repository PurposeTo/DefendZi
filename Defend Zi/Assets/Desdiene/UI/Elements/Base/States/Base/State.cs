using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private abstract class State : IStateEntryExitPoint, IProcessAccessorNotifier
        {
            private readonly IStateSwitcher<State> _stateSwitcher;
            private readonly IProcess _stateExecuting;

            protected State(UiElement it, IStateSwitcher<State> stateSwitcher)
            {
                It = it ?? throw new ArgumentNullException(nameof(it));
                _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
                _stateExecuting = new CyclicalProcess($"Выполнение состояния {GetType()}");
            }

            event Action IProcessNotifier.WhenRunning
            {
                add => _stateExecuting.WhenRunning += value;
                remove => _stateExecuting.WhenRunning -= value;
            }

            event Action IProcessNotifier.WhenCompleted
            {
                add => _stateExecuting.WhenCompleted += value;
                remove => _stateExecuting.WhenCompleted -= value;
            }

            event Action<IProcessAccessor> IProcessNotifier.OnChanged
            {
                add => _stateExecuting.OnChanged += value;
                remove => _stateExecuting.OnChanged -= value;
            }

            string IProcessAccessor.Name => _stateExecuting.Name;

            bool IProcessAccessor.KeepWaiting => _stateExecuting.KeepWaiting;


            void IStateEntryExitPoint.OnEnter()
            {
                _stateExecuting.Start();
                OnEnter();
            }

            void IStateEntryExitPoint.OnExit()
            {
                OnExit();
                _stateExecuting.Stop();
            }
            
            protected UiElement It { get; }

            public virtual Action SubscribeToWhenDisplayed(Action action, Action value) => action += value;
            public virtual Action SubscribeToWhenHidden(Action action, Action value) => action += value;

            public abstract IProcessAccessorNotifier Show();
            public abstract IProcessAccessorNotifier Hide();

            protected abstract void OnEnter();
            protected abstract void OnExit();

            protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
