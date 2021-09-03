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
    /// Need to be a singleton!
    /// </summary>
    public sealed class GlobalTimeScaler : MonoBehaviourExt, ITimeScaler, IProcesses
    {
        private TimeScaler _timeScaler;
        private ProcessesContainer _processes;

        [Inject]
        private void Constructor(GlobalTimeScaleAdapter globalTimeScaleAdapter)
        {
            _timeScaler = new TimeScaler(globalTimeScaleAdapter);
            _processes = new ProcessesContainer("Остановка глобального времени");
            OnChanged += UpdateTimeScaler;
        }

        public event Action<IMutableProcessGetter> OnChanged
        {
            add => _processes.OnChanged += value;
            remove => _processes.OnChanged -= value;
        }

        public void SetPause(bool pause) => _timeScaler.SetPause(pause);

        public void SetScale(float timeScale) => _timeScaler.SetScale(timeScale);

        public string Name => _processes.Name;

        public bool KeepWaiting => _processes.KeepWaiting;

        public void Add(IProcess process) => _processes.Add(process);

        public void Remove(IProcess process) => _processes.Remove(process);

        private void UpdateTimeScaler(IMutableProcessGetter process)
        {
            SetPause(process.KeepWaiting);
        }
    }
}
