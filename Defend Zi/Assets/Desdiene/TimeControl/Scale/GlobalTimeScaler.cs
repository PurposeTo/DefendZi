using Desdiene.TimeControl.Scale.Base;
using UnityEngine;

namespace Desdiene.TimeControl.Scale
{
    public sealed class GlobalTimeScaler : TimeScaler
    {
        public override float TimeScale { get => Time.timeScale; protected set { Time.timeScale = value; } }
    }
}
