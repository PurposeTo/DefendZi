using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Adapters;
using Desdiene.Types.Processes;
using Zenject;

namespace Desdiene.TimeControls
{
    /// Позволяет корректно взаимодействовать с изменением скорости игрового времени.
    /// Взаимодействовать с UnityEngine.Time можно только внутри ЖЦ monoBehaviour
    /// Need to be a global singleton!
    /// </summary>
    public sealed class GlobalTime : MonoBehaviourExt, ITime
    {
        private ITime _time;

        [Inject]
        private void Constructor(GlobalTimeScaleAdapter globalTimeScaleAdapter)
        {
            _time = new TimeSpeed(globalTimeScaleAdapter);
        }

        event Action ITimeNotificator.WhenStopped
        {
            add => _time.WhenStopped += value;
            remove => _time.WhenStopped -= value;
        }

        event Action ITimeNotificator.WhenRunning
        {
            add => _time.WhenRunning += value;
            remove => _time.WhenRunning -= value;
        }

        event Action ITimeNotificator.OnChanged
        {
            add => _time.OnChanged += value;
            remove => _time.OnChanged -= value;
        }

        float ITimeAccessor.Scale => _time.Scale;
        bool ITimeAccessor.IsPause => _time.IsPause;
        void ITimeMutator.Set(float timeScale) => _time.Set(timeScale);

        IProcess ITimePauseFactory.CreatePause(MonoBehaviourExt mono, string name) => _time.CreatePause(mono, name);
    }
}
