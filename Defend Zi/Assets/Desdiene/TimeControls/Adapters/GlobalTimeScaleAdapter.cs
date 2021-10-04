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

        event Action IPercentNotifier.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        bool IPercentAccessor.IsMin => Mathf.Approximately(TimeScale, 0);

        bool IPercentAccessor.IsMax => Mathf.Approximately(TimeScale, 1);

        float IPercentAccessor.Value => TimeScale;

        void IPercentMutator.Set(float percent) => SetTimeRefScale(percent);

        float IPercentMutator.SetAndGet(float percent)
        {
            SetTimeRefScale(percent);
            return TimeScale;
        }

        void IPercentMutator.SetMin() => SetTimeRefScale(0f);

        void IPercentMutator.SetMax() => SetTimeRefScale(1f);

        private float TimeScale => UnityEngine.Time.timeScale;

        private void SetTimeRefScale(float timeScale)
        {
            if (UnityEngine.Time.timeScale != timeScale)
            {
                UnityEngine.Time.timeScale = timeScale;
                OnChanged?.Invoke();
            }
        }
    }
}
