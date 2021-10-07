using System;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Elements
{
    public partial class UiElement
    {
        private abstract class State : IStateEntryExitPoint<UiElement>, IProcessAccessorNotifier
        {
            private readonly IStateSwitcher<State, UiElement> _stateSwitcher;
            private readonly UiElement _it;
            private readonly IProcess _stateExecuting;

            string IProcessAccessor.Name => _stateExecuting.Name;

            bool IProcessAccessor.KeepWaiting => _stateExecuting.KeepWaiting;

            protected State(UiElement it, IStateSwitcher<State, UiElement> stateSwitcher)
            {
                _it = it ?? throw new ArgumentNullException(nameof(it));
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

            void IStateEntryExitPoint<UiElement>.OnEnter(UiElement it)
            {
                _stateExecuting.Start();
                OnEnter(it);
            }

            void IStateEntryExitPoint<UiElement>.OnExit(UiElement it)
            {
                OnExit(it);
                _stateExecuting.Stop();
            }

            public virtual Action SubscribeToWhenDisplayed(Action action, Action value) => action += value;
            public virtual Action SubscribeToWhenHidden(Action action, Action value) => action += value;

            public IProcessAccessorNotifier Show() => Show(_it);
            public IProcessAccessorNotifier Hide() => Hide(_it);

            protected abstract IProcessAccessorNotifier Show(UiElement it);
            protected abstract IProcessAccessorNotifier Hide(UiElement it);

            protected abstract void OnEnter(UiElement it);
            protected abstract void OnExit(UiElement it);

            protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
        }
    }
}
