using Desdiene.TimeControl.Scale.Base;
using UnityEngine;

namespace Desdiene.TimeControl.Scale
{
    //Взаимодействовать с UnityEngine.Time только внутри ЖЦ monoBehaviour
    public sealed class GlobalTimeScaler : TimeScaler
    {
        private readonly MonoBehaviour monoBehaviour;

        public GlobalTimeScaler(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour != null
                ? monoBehaviour
                : throw new System.ArgumentNullException(nameof(monoBehaviour));
        }

        public override float TimeScale { get => Time.timeScale; protected set { Time.timeScale = value; } }
    }
}
