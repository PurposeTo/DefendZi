using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Adapters;
using Desdiene.TimeControls.Scales;
using Desdiene.Types.Processes;
using Zenject;

namespace Desdiene.TimeControls.Scalers
{
    /// Позволяет корректно взаимодействовать с изменением скорости игрового времени.
    /// Взаимодействовать с UnityEngine.Time можно только внутри ЖЦ monoBehaviour
    /// Need to be a global singleton!
    /// </summary>
    public sealed class GlobalTimeScaler : MonoBehaviourExt, ITimeScaler, IProcesses
    {
        private IManualTimeController _timeScaler;
        private IProcesses _processes;

        [Inject]
        private void Constructor(GlobalTimeScaleAdapter globalTimeScaleAdapter)
        {
            _timeScaler = new TimeScaler(globalTimeScaleAdapter);
            _processes = new ParallelProcesses("Остановка глобального времени");
            _processes.OnChanged += UpdateTimeScaler;
        }

        event Action IProcessNotifier.OnStarted
        {
            add => _processes.OnStarted += value;
            remove => _processes.OnStarted -= value;
        }

        event Action IProcessNotifier.OnCompleted
        {
            add => _processes.OnCompleted += value;
            remove => _processes.OnCompleted -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _processes.OnChanged += value;
            remove => _processes.OnChanged -= value;
        }

        void ITimeScaler.SetScale(float timeScale) => _timeScaler.SetScale(timeScale);

        string IProcessAccessor.Name => _processes.Name;

        bool IProcessAccessor.KeepWaiting => _processes.KeepWaiting;

        void IProcessesMutator.Add(IProcessAccessorNotifier[] processes) => _processes.Add(processes);
        
        void IProcessesMutator.Add(IProcessAccessorNotifier process) => _processes.Add(process);

        void IProcessesMutator.Remove(IProcessAccessorNotifier process) => _processes.Remove(process);

        void IProcesses.Clear() => _processes.Clear();

        private void UpdateTimeScaler(IProcessAccessor process)
        {
            _timeScaler.SetPause(process.KeepWaiting);
        }
    }
}
