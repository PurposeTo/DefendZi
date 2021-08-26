using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using UnityEngine;

namespace Desdiene.TimeControls.Scales
{
    /// Позволяет корректно взаимодействовать с изменением скорости игрового времени.
    /// Взаимодействовать с UnityEngine.Time можно только внутри ЖЦ monoBehaviour
    /// Need to be a singleton!
    /// </summary>
    public sealed class GlobalTimeScaler : MonoBehaviourExt, ITimeScaler
    {
        private IPercent timeScaleSaved;  // Сохраненное значение скорости времени
        private bool pause = false; // По умолчанию паузы нет

        public event Action<float> OnTimeScaleChanged;

        protected override void AwakeExt()
        {
            timeScaleSaved = new Percent(Time.timeScale);
        }

        public void SetPause(bool pause)
        {
            this.pause = pause;
            SetTimeScaleViaPause();
        }

        public void SetTimeScale(float timeScale) => timeScaleSaved.Set(timeScale);

        public float TimeScale => Time.timeScale;

        public bool IsPause => pause;

        private void SetTimeScaleViaPause()
        {
            if (pause) Time.timeScale = 0f;
            else Time.timeScale = timeScaleSaved.Value;
            OnTimeScaleChanged?.Invoke(Time.timeScale);
        }
    }
}
