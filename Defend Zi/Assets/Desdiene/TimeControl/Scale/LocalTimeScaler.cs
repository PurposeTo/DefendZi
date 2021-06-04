using Desdiene.TimeControl.Scale.Base;
namespace Desdiene.TimeControl.Scale
{
    public class LocalTimeScaler : TimeScaler, ITimeScaler
    {
        public override float TimeScale { get => timeScale; protected set { timeScale = value; } }
        private float timeScale = 1f; // По умолчанию время идет
    }
}
