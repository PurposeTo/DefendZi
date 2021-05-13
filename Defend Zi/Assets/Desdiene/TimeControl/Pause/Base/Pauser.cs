using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.TimeControl.Scale.Base;
using UnityEngine;

namespace Desdiene.TimeControl.Pause.Base
{
    public class Pauser : IPauser
    {
        private readonly TimeScaler timeScaler;
        private readonly List<IPausableTime> pausables = new List<IPausableTime>();

        public Pauser(TimeScaler timeScaler)
        {
            this.timeScaler = timeScaler;
        }

        public bool IsPause => timeScaler.IsPause;

        public void LogAllPausables()
        {
            string logMessage = "Pausable time list have next items:";
            pausables.ForEach(pausable => logMessage += $"\n{pausable.Name}. isPause: {pausable.IsPause}");
            Debug.Log(logMessage);
        }

        public void Add(IPausableTime pausable)
        {
            if (pausable == null) throw new ArgumentNullException(nameof(pausable));
            if (pausables.Contains(pausable))
            {
                Debug.LogWarning($"{this.GetType().Name} is already contains {pausable} with name {pausable.Name} in pausable list!");
            }
            if (pausables.Any(item => item.Name == pausable.Name))
            {
                Debug.LogError($"GlobalPause pausable list is already contains item with name {pausable.Name}, " +
                    "but it is not the same item!");
            }

            pausable.OnPauseChanged += UpdateTotalPause;
            pausables.Add(pausable);
            UpdateTotalPause();
        }

        public void Remove(IPausableTime pausable)
        {
            if (pausable == null) throw new ArgumentNullException(nameof(pausable));
            if (!pausables.Contains(pausable))
            {
                Debug.LogWarning($"{this.GetType().Name} is NOT contains {pausable} with name {pausable.Name} in pausable list!");
            }

            pausable.OnPauseChanged -= UpdateTotalPause;
            pausables.Remove(pausable);
            UpdateTotalPause();
        }

        private void UpdateTotalPause() => timeScaler.SetPause(CalculateTotalPause());

        private bool CalculateTotalPause()
        {
            return pausables.Any(item => item.IsPause);
        }
    }
}
