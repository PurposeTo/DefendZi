using System;
using System.Collections.Generic;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes.Cyclical.States;

namespace Desdiene.Types.Processes
{

    /// <summary>
    /// Цикличный процесс: после выключения может быть включен заново.
    /// </summary>
    public class CyclicalProcess : ICyclicalProcess
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private readonly string _name;

        public CyclicalProcess(string name)
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
                new Stopped(stateSwitcher, stateContext, _name),
                new Running(stateSwitcher, stateContext, _name)
            };
            stateSwitcher.Add(allStates);
            stateSwitcher.Switch<Stopped>();

            SubscribeEvents();
        }

        private event Action<IProcessAccessor> OnChanged;

        event Action ICyclicalProcessNotifier.OnStarted
        {
            add => CurrentState.OnStarted += value;
            remove => CurrentState.OnStarted -= value;
        }

        event Action ICyclicalProcessNotifier.OnStopped
        {
            add => CurrentState.OnStopped += value;
            remove => CurrentState.OnStopped -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        string IProcessAccessor.Name => _name;
        bool IProcessAccessor.KeepWaiting => CurrentState.KeepWaiting;

        void ICyclicalProcessMutator.Start() => CurrentState.Start();

        void ICyclicalProcessMutator.Stop() => CurrentState.Stop();

        private State CurrentState => _refCurrentState.Get() ?? throw new NullReferenceException(nameof(CurrentState));

        private void SubscribeEvents()
        {
            CurrentState.OnChanged += InvokeOnChanged;
        }

        private void InvokeOnChanged() => OnChanged?.Invoke(this);
    }
}
