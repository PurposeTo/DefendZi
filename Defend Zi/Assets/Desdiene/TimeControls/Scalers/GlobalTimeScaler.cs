using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Adapters;
using Desdiene.TimeControls.Scales;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using Zenject;

namespace Desdiene.TimeControls.Scalers
{
    /// Позволяет корректно взаимодействовать с изменением скорости игрового времени.
    /// Взаимодействовать с UnityEngine.Time можно только внутри ЖЦ monoBehaviour
    /// Need to be a global singleton!
    /// </summary>
    public sealed class GlobalTimeScaler : MonoBehaviourExt, ITimeScaler, ICyclicalProcesses
    {
        private IManualTimeController _timeScaler;
        private ICyclicalProcesses _processes;

        [Inject]
        private void Constructor(GlobalTimeScaleAdapter globalTimeScaleAdapter)
        {
            _timeScaler = new TimeScaler(globalTimeScaleAdapter);
            _processes = new CyclicalParallelProcesses("Остановка глобального времени");
            _processes.OnChanged += UpdateTimeScaler;
        }

        event Action ICyclicalProcessNotifier.WhenStarted
        {
            add => _processes.WhenStarted += value;
            remove => _processes.WhenStarted -= value;
        }

        event Action ICyclicalProcessNotifier.WhenStopped
        {
            add => _processes.WhenStopped += value;
            remove => _processes.WhenStopped -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _processes.OnChanged += value;
            remove => _processes.OnChanged -= value;
        }

        void ITimeScaler.SetScale(float timeScale) => _timeScaler.SetScale(timeScale);

        string IProcessAccessor.Name => _processes.Name;

        bool IProcessAccessor.KeepWaiting => _processes.KeepWaiting;

        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier[] processes) => _processes.Add(processes);
        
        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier process) => _processes.Add(process);

        void ICyclicalProcessesMutator.Remove(IProcessAccessorNotifier process) => _processes.Remove(process);

        void ICyclicalProcesses.Clear() => _processes.Clear();

        private void UpdateTimeScaler(IProcessAccessor process)
        {
            _timeScaler.SetPause(process.KeepWaiting);
        }
    }
}
