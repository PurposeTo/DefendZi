using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Adapters;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using Zenject;

namespace Desdiene.TimeControls.Scalers
{
    /// Позволяет корректно взаимодействовать с изменением скорости игрового времени.
    /// Взаимодействовать с UnityEngine.Time можно только внутри ЖЦ monoBehaviour
    /// Need to be a global singleton!
    /// </summary>
    public sealed class GlobalTime : MonoBehaviourExt, ITime, ICyclicalProcesses
    {
        private ITime _time;
        private ICyclicalProcesses _pauses;

        [Inject]
        private void Constructor(GlobalTimeScaleAdapter globalTimeScaleAdapter)
        {
            _time = new Time(globalTimeScaleAdapter);
            _pauses = new CyclicalParallelProcesses("Остановка глобального времени");
            SubscribeEvents();
        }

        event Action ICyclicalProcessNotifier.WhenStarted
        {
            add => _pauses.WhenStarted += value;
            remove => _pauses.WhenStarted -= value;
        }

        event Action ICyclicalProcessNotifier.WhenStopped
        {
            add => _pauses.WhenStopped += value;
            remove => _pauses.WhenStopped -= value;
        }

        event Action<IProcessAccessor> IProcessNotifier.OnChanged
        {
            add => _pauses.OnChanged += value;
            remove => _pauses.OnChanged -= value;
        }

        string IProcessAccessor.Name => _pauses.Name;

        bool IProcessAccessor.KeepWaiting => _pauses.KeepWaiting;

        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier[] processes) => _pauses.Add(processes);
        
        void ICyclicalProcessesMutator.Add(IProcessAccessorNotifier process) => _pauses.Add(process);

        void ICyclicalProcessesMutator.Remove(IProcessAccessorNotifier process) => _pauses.Remove(process);

        void ICyclicalProcesses.Clear() => _pauses.Clear();

        void ITime.Set(float timeScale) => _time.Set(timeScale);

        void ITime.Stop() => _time.Stop();

        void ITime.Run() => _time.Run();

        private void SubscribeEvents()
        {
            _pauses.WhenStarted += _time.Stop;
            _pauses.WhenStopped += _time.Run;
        }
    }
}
