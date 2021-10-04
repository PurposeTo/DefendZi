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
            _time = new Time(globalTimeScaleAdapter);
        }

        float ITimeAccessor.Scale => _time.Scale;

        event Action ITimeNotification.WhenStopped
        {
            add => _time.WhenStopped += value;
            remove => _time.WhenStopped -= value;
        }

        event Action ITimeNotification.WhenRunning
        {
            add => _time.WhenRunning += value;
            remove => _time.WhenRunning -= value;
        }

        event Action ITimeNotification.OnChanged
        {
            add => _time.OnChanged += value;
            remove => _time.OnChanged -= value;
        }

        void ITimeMutator.Set(float timeScale) => _time.Set(timeScale);

        ICyclicalProcess ITimePauseFactory.CreatePause(MonoBehaviourExt mono, string name) => _time.CreatePause(mono, name);
    }
}
