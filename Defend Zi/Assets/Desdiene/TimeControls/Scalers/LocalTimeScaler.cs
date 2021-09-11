using Desdiene.TimeControls.Scales;
using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scalers
{
    public sealed class LocalTimeScaler : IManualTimeController
    {
        private readonly IManualTimeController _timeScaler;

        public LocalTimeScaler()
        {
            IPercent timeScaleRef = new Percent(1f); // По умолчанию время идет
            _timeScaler = new TimeScaler(timeScaleRef);
        }

        public void SetPause(bool pause) => _timeScaler.SetPause(pause);

        public void SetScale(float timeScale) => _timeScaler.SetScale(timeScale);
    }
}
