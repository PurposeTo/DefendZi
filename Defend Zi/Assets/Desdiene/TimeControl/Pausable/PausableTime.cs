using System;
using System.Collections.Generic;
using System.Linq;
using Desdiene.TimeControl.Pauser;
using Desdiene.TimeControl.Scale;
using UnityEngine;

namespace Desdiene.TimeControl.Pausable
{
    public class PausableTime : ITimePausable
    {
        private readonly ITimeScaler timeScaler;
        private readonly List<ITimePauser> pausers = new List<ITimePauser>();

        public PausableTime(ITimeScaler timeScaler)
        {
            this.timeScaler = timeScaler;
        }

        public bool IsPause => timeScaler.IsPause;

        public void LogAllPausers()
        {
            string logMessage = $"Time pausers list have {pausers.Count} items. GlobalPause: {IsPause}";
            pausers.ForEach(pauser => logMessage += $"\nCause: {pauser.Name}. isPause: {pauser.IsPause}");
            Debug.Log(logMessage);
        }

        public void Add(ITimePauser pauser)
        {
            if (pauser == null) throw new ArgumentNullException(nameof(pauser));
            if (pausers.Contains(pauser))
            {
                Debug.LogWarning($"{this.GetType().Name} is already contains {pauser} with name {pauser.Name} in pausable list!");
            }
            if (pausers.Any(item => item.Name == pauser.Name))
            {
                Debug.LogError($"GlobalPause pausable list is already contains item with name {pauser.Name}, " +
                    "but it is not the same item!");
            }

            pauser.OnPauseChanged += UpdateTotalPause;
            pausers.Add(pauser);
            UpdateTotalPause();
        }

        public void Remove(ITimePauser pauser)
        {
            if (pauser == null) throw new ArgumentNullException(nameof(pauser));
            if (!pausers.Contains(pauser))
            {
                Debug.LogWarning($"{this.GetType().Name} is NOT contains {pauser} with name {pauser.Name} in pausable list!");
            }

            pauser.OnPauseChanged -= UpdateTotalPause;
            pausers.Remove(pauser);
            UpdateTotalPause();
        }

        private void UpdateTotalPause() => timeScaler.SetPause(CalculateTotalPause());

        private bool CalculateTotalPause()
        {
            return pausers.Any(item => item.IsPause);
        }
    }
}
