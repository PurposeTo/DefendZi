using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Percents;
using UnityEngine;

namespace Desdiene.TimeControls.Adapters
{
    /// <summary>
    /// Позволяет привести Time.timeScale к интерфейсу IPercent.
    /// Need to be a global singleton!
    /// It must be used ONLY in GlobalTimeScaler!
    /// </summary>
    public class GlobalTimeScaleAdapter : MonoBehaviourExt, IPercent
    {
        private event Action OnChanged;

        event Action IPercentNotifier.OnValueChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        bool IPercentGetter.IsMin => Mathf.Approximately(TimeScale, 0);

        bool IPercentGetter.IsMax => Mathf.Approximately(TimeScale, 1);

        float IPercentGetter.Value => TimeScale;

        void IPercentSetter.Set(float percent) => SetTimeRefScale(percent);

        float IPercentSetter.SetAndGet(float percent)
        {
            SetTimeRefScale(percent);
            return TimeScale;
        }

        private float TimeScale => Time.timeScale;

        private void SetTimeRefScale(float timeScale)
        {
            if (Time.timeScale != timeScale)
            {
                Time.timeScale = timeScale;
                OnChanged?.Invoke();
            }
        }
    }
}
