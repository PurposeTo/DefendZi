using Desdiene.Types.Percents;

namespace Desdiene.TimeControls.Scalers
{
    public sealed class LocalTime : ITime
    {
        private readonly ITime _time;

        public LocalTime()
        {
            IPercent timeScaleRef = new Percent(1f); // По умолчанию время идет
            _time = new Time(timeScaleRef);
        }

        void ITime.Set(float timeScale) => _time.Set(timeScale);

        void ITime.Stop() => _time.Stop();

        void ITime.Run() => _time.Run();
    }
}
