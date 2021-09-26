using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes.Linear.States;

namespace Desdiene.Types.Processes
{

    /// <summary>
    /// Линейный процесс: после выключения не может быть включен.
    /// Обязательно ждать, если не не был запущен на момент обращения.
    /// </summary>
    public class LinearProcess : ILinearProcess
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private readonly string _name;

        public LinearProcess(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" Can't be null or empty.", nameof(name));
            }

            _name = name;
            StateContext stateContext = new StateContext();
            StateSwitcher<State> stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Created(stateSwitcher, stateContext, _name),
                new Running(stateSwitcher, stateContext, _name),
                new Completed(stateSwitcher, stateContext, _name)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Created>();

            SubscribeEvents();
        }

        private event Action<IProcessAccessor> OnChanged;

        event Action ILinearProcessNotifier.WhenStarted
        {
            add => CurrentState.OnStarted += value;
            remove => CurrentState.OnStarted -= value;
        }

        event Action ILinearProcessNotifier.WhenCompleted
        {
            add => CurrentState.OnCompleted += value;
            remove => CurrentState.OnCompleted -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        string IProcessAccessor.Name => _name;
        bool IProcessAccessor.KeepWaiting => CurrentState.KeepWaiting;

        void ILinearProcessMutator.Start() => CurrentState.Start();

        void ILinearProcessMutator.Stop() => CurrentState.Complete();

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        private void SubscribeEvents()
        {
            CurrentState.OnChanged += InvokeOnChanged;
        }

        private void InvokeOnChanged() => OnChanged?.Invoke(this);
    }
}
